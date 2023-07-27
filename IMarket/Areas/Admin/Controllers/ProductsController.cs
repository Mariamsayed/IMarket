using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IMarket.InterFace;
using IMarket.Models;
using IMarket.Models.Db;
using DbContext = IMarket.Models.DbContext;

namespace IMarket.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private DbContext db = new DbContext();

        private readonly IProduct _Product;

        public ProductsController(IProduct product)
        {
            _Product = product;
        }



        public ActionResult Index()
        {
            var product_ = _Product.GetAllProducts();
            return View();
        }
        
        // GET: Admin/Products/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _Product.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.ProductCategory.Where(x=>x.IsDeleted==false), "Id", "Name");
            return View();
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Product product , HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    var fileName = Image.FileName;
                    Image.SaveAs(Server.MapPath("~/Assets/" + fileName));
                    product.Image = fileName;
                }
                 _Product.AddProduct(product);

                TempData["Product"] = "Car has been added ";
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.ProductCategory.Where(x=>x.IsDeleted==false), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product =  _Product.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.ProductCategory.Where(x => x.IsDeleted == false), "Id", "Name", product.CategoryId);
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Product product, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    var fileName = Image.FileName;
                    Image.SaveAs(Server.MapPath("~/Assets/" + fileName));
                    product.Image = fileName;
                }
                _Product.UpdateProduct(product);

                TempData["Product"] = $"car {product.Name} has been Updated ";
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.ProductCategory.Where(x=>x.IsDeleted==false), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = _Product.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _Product.DeleteProduct(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
