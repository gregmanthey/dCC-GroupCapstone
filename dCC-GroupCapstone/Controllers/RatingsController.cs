using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dCC_GroupCapstone.Controllers
{
    public class RatingsController : Controller
    {
        // GET: Ratings
        public ActionResult Index()
        {
            return View();
        }

        // GET: Ratings/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ratings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ratings/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ratings/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ratings/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

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
            return View();
        }

        // POST: Ratings/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
