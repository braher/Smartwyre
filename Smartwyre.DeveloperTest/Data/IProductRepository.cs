using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Data
{
    public  interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();

        Product GetProductById(int productId);

        Product GetProductByIdentifier(string productIdentifier);

        void AddProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(int productId);

        void Save();
    }
}
