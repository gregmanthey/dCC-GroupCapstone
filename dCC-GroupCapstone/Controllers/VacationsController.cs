using dCC_GroupCapstone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
            //context.Configuration.LazyLoadingEnabled = false;
            //var hotels = context.Hotels;
            //var activities = context.Activities;
            var userId = User.Identity.GetUserId();
            //var test = context.Hotels.Select(h => new { h, h.Name});

            var vacations = context.Vacations.ToList();
            //var vacations = (from v in context.Vacations
            //         join h in context.Hotels on v.SavedHotel equals h.Id
            //         orderby v.VacationName
            //         select new
            //         {
            //             v.VacationName,
            //             v.Cost,
            //             v.LocationQueried,
            //             h.Name
            //         }).ToList();
            return View(vacations);

            //Customer customer = context.Customers.SingleOrDefault(c => c.UserId == userId);
            
            //foreach (Vacation vacation in vacations)
            //{
            //    if (vacation.IsPrivate == false || customer.SavedVacations.Contains(vacation))
            //    {
                    
            //    }
            //}
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
                vacationInDb.VacationName = vacation.VacationName;
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
        // Filter - lodging/other
        // Filter - include interests/don't
    }
}
