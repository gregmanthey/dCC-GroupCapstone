using dCC_GroupCapstone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dCC_GroupCapstone.Controllers
{
    public class RatingsController : Controller
    {
        ApplicationDbContext context;
        public RatingsController()
        {
            context = new ApplicationDbContext();
        }

        // GET: Ratings
        public ActionResult Index()
        {
            var ratingsList = context.Ratings.ToList();
            return View(ratingsList);
        }

        // GET: Ratings/Details/5
        // Is this even necessary to get an individual Rating Details? Maybe for viewing other reviews from the same user?
        public ActionResult Details(Vacation vacation)
        {


            return View();
        }

        // GET: Ratings/Create
        // Creating from some icon/button on GUI.
        // Five icons, all set to value forms?
        public ActionResult Create(int vacationId, int value)
        {
            
            var userId = User.Identity.GetUserId();
            if(userId != null)
            {
                
                Customer customer = context.Customers.Where(c => c.UserId == userId).SingleOrDefault();
                Rating rating = new Rating();
                rating.VacationId = vacationId;
                rating.CustomerId = customer.Id;
                rating.RatingValue = value;
                context.Ratings.Add(rating);
                context.SaveChanges();
                return View(rating);
            }
            else{
                Console.WriteLine("You must register for an account to rate vacations.");
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Ratings/Create
        [HttpPost]
        public ActionResult Create(Rating rating)
        {
            try
            {
                // Redirect to the page they were already on
                context.Ratings.Add(rating);
                context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ratings/Edit/5
        public ActionResult Edit(int id)
        {
            Rating rating = context.Ratings.Where(r => r.VacationId == id).SingleOrDefault();
            return View(rating);
        }

        // POST: Ratings/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Rating rating)
        {
            try
            {
                Rating editRating = context.Ratings.Find(id);
                editRating.RatingValue = rating.RatingValue;
                context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ratings/Delete/5
        public ActionResult Delete(int id)
        {
            Rating rating = context.Ratings.Where(r => r.VacationId == id).SingleOrDefault();
            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Rating rating)
        {
            try
            {
                context.Ratings.Remove(rating);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // Methods
        // AverageRating
        public void AverageRatings(Vacation vacation)
        {
            var ratings = context.Ratings.Where(r => r.VacationId == vacation.Id)
                .Select(r => r.RatingValue).Average();
        }
    }
}
