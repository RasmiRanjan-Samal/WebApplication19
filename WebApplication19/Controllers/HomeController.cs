using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication19.Models;
using WebApplication19.ViewModels;
using Scrypt;

namespace WebApplication19.Controllers
{
    public class HomeController : Controller
    {
        private Model1 db = new Model1();


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(LoginVM loginVM)
        {
            ScryptEncoder encoder = new ScryptEncoder();

            // check if user already register
            var registeredUser = (from c in db.Customers
                                 where c.Username.Equals(loginVM.Username)
                                 select c).SingleOrDefault();
            if (registeredUser != null)
            {
                ViewBag.Error = "This username already registered.";
                return View();
            }

            // check if customerId exists in the customer table.
            var validCustomer = (from c in db.Customers
                                 where c.CustomerID.Equals(loginVM.CustomerId)
                                 select c).SingleOrDefault();

            if (validCustomer == null)
            {
                ViewBag.Error = "Customer Id cannot be found.";
                return View();
            }
            validCustomer.Username = loginVM.Username;
            validCustomer.Password = encoder.Encode(loginVM.Password);

            db.SaveChanges();

            ViewBag.Error = "Registered successfully. Please login.";

            return View();
        }

       
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM loginVM)
        {
            ScryptEncoder encoder = new ScryptEncoder();

            var validCustomer = (from c in db.Customers
                                 where c.Username.Equals(loginVM.Username)
                                 select c).SingleOrDefault();

            if (validCustomer == null)
            {
                ViewBag.Error = "Username or password is invalid.";
                return View();
            }

            bool isValidCustomer = encoder.Compare(loginVM.Password, validCustomer.Password);
            if (isValidCustomer)
            {
                Session["customerID"] = validCustomer.CustomerID;
                return RedirectToAction("Order");
            }
            else
            {
                ViewBag.Error = "Username or password is invalid.";
                return View();
            }
           
        }

        public ActionResult Order()
        {
            var id = Session["customerID"];
            var filteredOrder = from o in db.Orders
                                where o.CustomerID.Equals(id.ToString())
                                select o;
            return View(filteredOrder);
        }


        public ActionResult OrderDetails()
        {

            var id = Session["customerID"];
            var filteredOrderDetails = from o in db.Orders
                                join od in db.Order_Details
                                on o.OrderID equals od.OrderID
                                where o.CustomerID.Equals(id.ToString())
                                select od;
            return View(filteredOrderDetails);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}