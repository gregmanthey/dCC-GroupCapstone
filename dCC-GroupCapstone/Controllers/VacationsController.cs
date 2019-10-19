using dCC_GroupCapstone.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace dCC_GroupCapstone.Controllers
{
    public class VacationsController : Controller

    {
        ApplicationDbContext context;

        public VacationsController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Vacation
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var vacations = context.Vacations.ToList();
            var activities = context.Activities.ToList();
            var hotels = context.Hotels.ToList();
            var tuple = Tuple.Create(vacations, activities, hotels);
            return View(tuple);
        }

        public ActionResult UserIndex()
        {
            var userId = User.Identity.GetUserId();
            var customer = context.Customers.SingleOrDefault(c => c.UserId == userId);
            var vacations = context.Vacations.Where(v => v.CustomerCreated == customer.Id).ToList();
            return View(vacations);
        }

        public ActionResult TopIndex()
        {
            var vacations = context.Vacations.OrderByDescending(v => v.AverageRating).ToList();
            return View(vacations);
        }

        // GET: Vacation/Details/5
        public ActionResult Details(int id)
        {
            return View(context.Vacations.Where(v => v.Id == id).FirstOrDefault());
        }

        // GET LOCATION
        public ActionResult StartCreate()
        {
            return View();
        }

        // POST LOCATION
        [HttpPost]
        public async Task<ActionResult> StartCreate(Vacation vacation)
        {
            var selectedLocation = vacation.LocationQueried;
            string geocodedLocation = await GeocodeFromLocationToLatLongString(selectedLocation);
            return RedirectToAction("Create", new { latLong = geocodedLocation, locationName = selectedLocation });
        }

        // GET: Vacation/Create
        public async Task<ActionResult> Create( string latLong, string locationName )
        {
            // take latlong and put into lists of hotels/activities/bleh
            var currentUserId = User.Identity.GetUserId();
            var currentCustomer = context.Customers.FirstOrDefault(c => c.UserId == currentUserId);
            var customerInterests = currentCustomer.Interests;
            if (customerInterests is null)
            {
                customerInterests = new List<string>();
            }
            customerInterests.Add("Lodging");
            var hotels = new List<Hotel>();
            var activities = new List<Activity>();
            foreach (var interest in customerInterests)
            {
                var listOfApiSearchTypes = GetPlacesApiTypeList(interest);
                foreach (var searchType in listOfApiSearchTypes)
                {
                    var results = await PlacesTypeApiSearch(searchType, latLong);
                    if (listOfApiSearchTypes == PlaceApiTypes.Lodging)
                    {
                        foreach (var result in results)
                        {
                            var hotel = new Hotel();
                            hotel.Name = result.name;
                            hotel.PlaceId = result.place_id;
                            hotel.Price = result.price_level;
                            hotel.LatLong = result.geometry.location.lat.ToString() + "," + result.geometry.location.lng.ToString();
                            //hotel.GoogleRating = result.rating;
                            hotels.Add(hotel);
                        }
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            var activity = new Activity();
                            activity.Name = result.name;
                            activity.PlaceId = result.place_id;
                            activity.Price = result.price_level;
                            activity.LatLong = result.geometry.location.lat.ToString() + "," + result.geometry.location.lng.ToString();
                            //activity.GoogleRating = result.rating;
                            activities.Add(activity);
                        }
                    }
                }
            }
            Vacation vacation = new Vacation() { LocationQueried = locationName, VacationName = "My New Vacation"};
            var tupleResult = new Tuple<Vacation, IEnumerable<Hotel>, IEnumerable<Activity>>(vacation, hotels, activities);
            return View(tupleResult);
        }

        // POST: Vacation/Create
        [HttpPost]
        public ActionResult Create(Vacation vacation)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var customer = context.Customers.SingleOrDefault(c => c.UserId == userId);
                vacation.CustomerCreated = customer.Id;
                context.Vacations.Add(vacation);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vacation/Edit/5
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            Customer currentCustomer = context.Customers.SingleOrDefault(c => c.UserId == userId);
            var vacationEdit = context.Vacations.Find(id);

            if (currentCustomer.Id == vacationEdit.CustomerCreated)
            {
                return View(vacationEdit);
            }
            else
            {
                int vacationId = CopyToNewUser(vacationEdit);
                return RedirectToAction("Edit", "Vacations", new { id = vacationId });
            }
        }
        public int CopyToNewUser(Vacation vacation)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = context.Customers.SingleOrDefault(u => u.UserId == currentUserId);
            vacation.CustomerCreated = currentUser.Id;
            var newVacation = context.Vacations.Add(vacation);
            context.SaveChanges();
            return newVacation.Id;
        }

        // POST: Vacation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Vacation vacationCreated)
        {
            var userId = User.Identity.GetUserId();
            Customer currentCustomer = context.Customers.SingleOrDefault(c => c.UserId == userId);
            
            try
            {
                var vacationInDb = context.Vacations.Find(id);
                vacationInDb.LocationQueried = vacationCreated.LocationQueried;
                vacationInDb.SavedHotel = vacationCreated.SavedHotel;
                vacationInDb.Cost = vacationCreated.Cost;
                vacationInDb.CustomerCreated = context.Customers.SingleOrDefault(c => c.UserId == userId).Id;
                vacationInDb.VacationName = vacationCreated.VacationName;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vacation/Delete/5
        public ActionResult Delete(int id)
        {
            return View(context.Vacations.Find(id));
        }

        // POST: Vacation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Vacation vacation)
        {
            try
            {
                Vacation selectedVacation = context.Vacations.Find(id);
                context.Vacations.Remove(selectedVacation);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // TODO
        // Methods
        // API Call - get location
        public List<string> GetPlacesApiTypeList(string interest)
        {
            switch (interest)
            {
                case "ReligiousPlaces":
                    return PlaceApiTypes.ReligiousPlaces;
                case "Food":
                    return PlaceApiTypes.Food;
                case "Shopping":
                    return PlaceApiTypes.Shopping;
                case "TouristAttractions":
                    return PlaceApiTypes.TouristAttractions;
                case "NightLife":
                    return PlaceApiTypes.NightLife;
                case "Outdoors":
                    return PlaceApiTypes.Outdoors;
                case "Lodging":
                    return PlaceApiTypes.Lodging;
                default:
                    return null;
            }
        }
        public async Task<List<GooglePlacesApiJson.Result>> LoopThroughPlaceTypes(List<string> types, string LatLong)
        {
            var results = new List<GooglePlacesApiJson.Result>();
            foreach (string item in types)
            {
                var searchResults = await PlacesTypeApiSearch(item, LatLong);
                results.AddRange(searchResults);
            }
            return results;
        }

        public async Task<List<GooglePlacesApiJson.Result>> PlacesTypeApiSearch(string type, string LatLong)
        {
            var http = new HttpClient();
            var url = String.Format("https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={0}&radius=2000&type={1}&key={2}", LatLong, type, Keys.GoogleApiKey);
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var jsonData = JsonConvert.DeserializeObject<GooglePlacesApiJson.Rootobject>(result);
            var resultsList = new List<GooglePlacesApiJson.Result>();

            for (int i = 0; i < jsonData.results.Count(); i++)
            {
                resultsList.Add(jsonData.results[i]);
            }

            return resultsList;
        }

        public async Task<string> GeocodeFromLocationToLatLongString(string location)
        {
            var http = new HttpClient();
            var url = String.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}", location, Keys.GoogleApiKey);
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var jsonData = JsonConvert.DeserializeObject<GoogleMapsApiJson.Rootobject>(result);
            var latLong = jsonData.results[0].geometry.location.lat.ToString() + "," + jsonData.results[0].geometry.location.lng.ToString();

            return latLong;
        }

        public string GenerateStaticMapUrlForVacation(Vacation vacation)
        {
            var url = new StringBuilder();
            url.Append("https://maps.googleapis.com/maps/api/staticmap?");
            url.Append("size=600x600&maptype=hybrid&markers=color:blue|label:A|");
            //var activities = context.Activities.Where(a => a.Id == vacation.Activities.).ToList();
            if (vacation.Activities != null)
            {
                foreach (var activity in vacation.Activities)
                {
                    var activityLocation = context.Activities.FirstOrDefault(a => a.Id == activity.Id).LatLong;
                    url.Append($"{activityLocation}|");
                }
            }
            
            var hotelLocation = context.Hotels.FirstOrDefault(h => h.Id == vacation.SavedHotel).LatLong;
            url.Append($"&markers=color:orange|label:H|{hotelLocation}&");
            url.Append("key=" + Keys.GoogleApiKey);
            return url.ToString();
        }
        //https://maps.googleapis.com/maps/api/staticmap?center=Brooklyn+Bridge,New+York,NY&zoom=13&size=600x300&maptype=roadmap&markers=color:blue%7Clabel:S%7C40.702147,-74.015794&markers=color:green%7Clabel:G%7C40.711614,-74.012318&markers=color:red%7Clabel:C%7C40.718217,-73.998284&key=YOUR_API_KEY
        // Filter - lodging/other
        // Filter - include interests/don't
    }
}
