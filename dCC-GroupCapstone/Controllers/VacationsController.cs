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
            
            var vacations = context.Vacations.ToList();
            var userId = User.Identity.GetUserId();
            Customer customer = context.Customers.SingleOrDefault(c => c.UserId == userId);
            
            //foreach (Vacation vacation in vacations)
            //{
            //    if (vacation.IsPrivate == false || customer.SavedVacations.Contains(vacation))
            //    {
                    
            //    }
            //}
            return View(vacations);
        }

        // GET: Vacation/Details/5
        public ActionResult Details(int id)
        {
            return View(context.Vacations.Where(v => v.Id == id).FirstOrDefault());
        }

        // GET: Vacation/Create
        public ActionResult Create()
        {
            Vacation vacation = new Vacation();
            return View(vacation);
        }

        // POST: Vacation/Create
        [HttpPost]
        public ActionResult Create(Vacation vacation)
        {
            try
            {
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
            var vacationEdit = context.Vacations.Find(id);
            return View(vacationEdit);
        }

        // POST: Vacation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Vacation vacation)
        {
            try
            {
                var vacationInDb = context.Vacations.Find(id);
                vacationInDb.Name = vacation.Name;
                vacationInDb.SavedHotel = vacation.SavedHotel;
                vacationInDb.LocationQueried = vacation.LocationQueried;
                vacationInDb.Cost = vacation.Cost;
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
        public async Task<List<GoogleJsonResults.Result>> LoopThroughPlaceTypes(List<string> types, string LatLong)
        {
            var results = new List<GoogleJsonResults.Result>();
            foreach (string item in types)
            {
                var searchResults = await PlacesTypeApiSearch(item, LatLong);
                results.AddRange(searchResults);
            }
            return results;
        }

        public async Task<List<GoogleJsonResults.Result>> PlacesTypeApiSearch(string type, string LatLong)
        {
            var http = new HttpClient();
            var url = String.Format("https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={0}&radius=2000&type={1}&key={2}", LatLong, type, Keys.GoogleApiKey);
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var jsonData = JsonConvert.DeserializeObject<GoogleJsonResults.Rootobject>(result);
            var resultsList = new List<GoogleJsonResults.Result>();

            for (int i = 0; i < jsonData.results.Count(); i++)
            {
                resultsList.Add(jsonData.results[i]);
            }

            return resultsList;
        }
        // Filter - lodging/other
        // Filter - include interests/don't
    }
}
