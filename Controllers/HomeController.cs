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
            var customers = entities.Customers.ToList();
            return Json(customers, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PostCustomers(Customer cust)
        {
            entities.Customers.Add(cust);
            entities.SaveChanges();
            return Json(cust, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveCustomers(int id )
        {
            var c= entities.Customers.SingleOrDefault(e=>e.id==id);
            entities.Customers.Remove(c);
            entities.SaveChanges();
            return Json(c, JsonRequestBehavior.AllowGet);
        }
    }
}