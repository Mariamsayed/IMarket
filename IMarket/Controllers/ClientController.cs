using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMarket.Models;

namespace IMarket.Controllers
{
    public class ClientController : Controller
    {
        private readonly DbContext db =new DbContext();
        // GET: Client

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Customer customer)
        {
            customer.IsDeleted = false;
            customer.Create=DateTime.Now;
            customer.LastUpdate=DateTime.Now;


            db.Customers.Add(customer);
            db.SaveChanges();
            TempData["Customer"]="Customer has been Added";
            Session["UserId"]=customer.Id;
            return RedirectToAction ("Index","Home");

        }

        public ActionResult Logout()
        {
             Session.Clear();
             Session.Abandon();
             return RedirectToAction("Index","Home");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email,string Password)
        {
            var check = db.Customers.FirstOrDefault(x => x.Email == Email && x.Password == Password);


            if (check==null)
            {
                TempData["user"] = "login incorrect check your user name or password";
                return View();
            }


            Session["UserId"]=check.Id;


            return RedirectToAction("Index","Home");
        }



    }
}