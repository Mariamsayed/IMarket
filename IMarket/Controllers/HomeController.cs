using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMarket.Models;

namespace IMarket.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new DbContext();
            var Pro = db.Product.Where(x => x.IsDeleted == false).ToList();
            return View(Pro);
        }

    }
}