using dCC_GroupCapstone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
            
            foreach (Vacation vacation in vacations)
            {
                if (vacation.IsPrivate == false || customer.SavedVacations.Contains(vacation))
                {
                    
                }
            }
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
