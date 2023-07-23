using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMarket.Models;

namespace IMarket.Controllers
{
    public class OurProductsController : Controller
    {
        private DbContext db = new DbContext();

        // GET: OurProducts
        public ActionResult Index()
        {
            var Pro = db.Product.Where(x => x.IsDeleted == false).ToList();
            return View(Pro);
        }




        public ActionResult Details(int id=default)
        {
            if (id==default)
            {
                return HttpNotFound();
            }
            var Pro = db.Product.FirstOrDefault(x => x.Id == id);
            return View(Pro);
        }




        [HttpPost]
        public ActionResult Request(string Msg, int ProductId)
        {
            var user = Convert.ToInt16(Session["UserId"]);
            if (user == 0)
            {
                return RedirectToAction("Login", "Client");
            }

            var mes = new Request();
            mes.CustomerId = Convert.ToInt16(Session["UserId"]);
            mes.ProductId = ProductId;
            mes.Create=DateTime.Now;
            mes.LastUpdate=DateTime.Now;
            mes.IsDeleted = false;
            mes.Msg = Msg;
            db.Requests.Add(mes);
            db.SaveChanges();
            TempData["Requests"] = "Request has been send to The Admin";
            return RedirectToAction("Details","OurProducts",new {id=mes.ProductId});
        }
    }
}