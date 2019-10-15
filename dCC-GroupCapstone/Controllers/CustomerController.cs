using dCC_GroupCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dCC_GroupCapstone.Controllers
{
    public class CustomerController : Controller
    {
        ApplicationDbContext context;
        public CustomerController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Customer
        public ActionResult Index()
        {
            return View(context.Customers.ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            Customer customerDetails = context.Customers.Where(c => c.Id == id).SingleOrDefault();
            return View(customerDetails);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            Customer createCustomer = new Customer();
            return View(createCustomer);
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer Customers)
        {
            try
            {
                // TODO: Add insert logic here
                context.Customers.Add(Customers);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            Customer editCustomer = context.Customers.Where(e => e.Id == id).SingleOrDefault();
            return View(editCustomer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Customer customer)
        {
            try
            {
                // TODO: Add update logic here
                var editCustomer = context.Customers.Find(id);
                editCustomer.FirstName = customer.FirstName;
                editCustomer.LastName = customer.LastName;
                // Circle back to Interest and SavedVacations...
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            Customer customer = context.Customers.Where(c => c.Id == id).SingleOrDefault();
            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Customer customer)
        {
            try
            {
                // TODO: Add delete logic here
                context.Customers.Remove(customer);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
