using dCC_GroupCapstone.Models;
using Microsoft.AspNet.Identity;
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
            var userId = User.Identity.GetUserId();
            var customerId = context.Customers.FirstOrDefault(c => c.UserId == userId).Id;
            return RedirectToAction("Details", new { id = customerId });
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
        public ActionResult Create(Customer customer)
        {
            try
            {
                // TODO: Add insert logic here
                customer.UserId = User.Identity.GetUserId();
                //var interests = customer.Interests;
                //foreach (var interest in interests)
                //{
                //    var existingInterest = context.Interests.SingleOrDefault(i => i.Name == interest.Name);
                //    if (existingInterest is null)
                //    {
                //        context.Interests.Add(interest);
                //    }
                //}
                context.Customers.Add(customer);
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
                editCustomer.Interests = customer.Interests;
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
            Customer deleteCustomer = context.Customers.Where(d => d.Id == id).SingleOrDefault();
            return View(deleteCustomer);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Customer customer)
        {
            try
            {
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
