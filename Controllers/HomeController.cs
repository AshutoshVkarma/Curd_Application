using Curd_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Curd_Application.Controllers
{
    public class HomeController : Controller
    {
        private CustomerDbEntities entities;
        public HomeController()
        {
            entities = new CustomerDbEntities();
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCustomers() 
        {
            List<Customer> customers = entities.Customers.ToList<Customer>();
            return Json(new { data = customers }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult AddCustomers(int id = 0)
        {
            if (id == 0) 
            {
                return View(new Customer());
            }
            else
            {
                return View(entities.Customers.Where(e => e.id == id).FirstOrDefault<Customer>());
            }
        }


        [HttpPost]
        public ActionResult AddCustomers(Customer cust)
        {
            if (cust.id == 0)
            {
                entities.Customers.Add(cust);
                entities.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                entities.Entry(cust).State = System.Data.Entity.EntityState.Modified;
                entities.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPost]
        public ActionResult RemoveCustomer(int id)
        {
            Customer cust = entities.Customers.Where(e => e.id == id).FirstOrDefault<Customer>();
            entities.Customers.Remove(cust);
            entities.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}