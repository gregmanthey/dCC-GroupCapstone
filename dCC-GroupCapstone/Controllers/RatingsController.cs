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

        // GET: Ratings/Create
        // Creating from some icon/button on GUI.
        // Five icons, all set to value forms?
        public ActionResult Create(int vacationId)
        {
            Rating newRating = new Rating();
            newRating.VacationId = vacationId;
            return View(newRating);
        }

        // POST: Ratings/Create
        [HttpPost]
        public ActionResult Create(Rating rating)
        {           
            var userId = User.Identity.GetUserId();
            Customer customer = context.Customers.Where(c => c.UserId == userId).FirstOrDefault();
            rating.CustomerId = customer.Id;
            if (userId != null)
            {
                Rating existingRating = context.Ratings.Where(r => r.CustomerId == rating.CustomerId && r.VacationId == rating.VacationId).SingleOrDefault();
                if (existingRating == null)
                {
                    context.Ratings.Add(rating);
                    context.SaveChanges();
                    Vacation vacation = context.Vacations.SingleOrDefault(v => v.Id == rating.VacationId);
                    vacation.AverageRating = AverageRatings(vacation);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else if (existingRating != null)
                {
                    //return RedirectToAction("Edit", "Ratings", new { id = vacationId, rating = newRating});
                    return Edit(rating.CustomerId, rating);
                }
            }
            else
            {
                Console.WriteLine("You must be logged in to do that");
                return RedirectToAction("Register", "Account");
            }
            return null;
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
                Rating editRating = context.Ratings.SingleOrDefault(r => r.CustomerId == rating.CustomerId && r.VacationId == rating.VacationId);
                editRating.RatingValue = rating.RatingValue;
                context.SaveChanges();
                Vacation vacation = context.Vacations.SingleOrDefault(v => v.Id == rating.VacationId);
                vacation.AverageRating = AverageRatings(vacation);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
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
        public double AverageRatings(Vacation vacation)
        {
            var ratings = context.Ratings.Where(r => r.VacationId == vacation.Id)
                .Select(r => r.RatingValue).Average();
            return ratings;
        }
    }
}
