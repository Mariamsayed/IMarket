using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using IMarket.Models.Db;
using DbContext = IMarket.Models.DbContext;

namespace IMarket.InterFace
{
    public class ProductService:IProduct
    {

        private readonly DbContext _db;

        public ProductService(DbContext dbContext)
        {
            _db = dbContext;
        }


        public IEnumerable<Product> GetAllProducts()
        {
            var Product = _db.Product.Where(x => x.IsDeleted == false).ToList();
            return Product;
        }

        public Product GetProductById(int productId)
        {
            var _pro = _db.Product.FirstOrDefault(x => x.Id == productId);
            return _pro;
        }

        public void AddProduct(Product product)
        {


            product.IsDeleted = false;
            product.Create = DateTime.Now;
            product.LastUpdate = DateTime.Now;

            _db.Product.Add(product);
            _db.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            product.LastUpdate = DateTime.Now;
            _db.Entry(product).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void DeleteProduct(int productId)
        {
            var product = _db.Product.Find(productId);
            product.IsDeleted = true;
            _db.Product.AddOrUpdate(product);
            _db.SaveChanges();
        }
    }
}