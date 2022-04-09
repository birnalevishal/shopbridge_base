using Shopbridge_base.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task<Product> Addproduct(Product product);
        Task<Product> UpdateProduct(Product product, int id);
        Task DeleteProduct(int id);
        Task<bool> ProductExists(int id);
    }
}
