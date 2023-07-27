using System.Collections.Generic;
using IMarket.Models.Db;

namespace IMarket.InterFace
{
    public interface IProduct
    {
        IEnumerable<Product> GetAllProducts();
         Product GetProductById(int productId);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int productId );
    }
}
