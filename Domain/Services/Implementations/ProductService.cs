using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Data;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> logger;
        private readonly Shopbridge_Context db;

        public ProductService(Shopbridge_Context _db)
        {
            this.db = _db;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            IList<Product> productList = await db.Product.ToListAsync();
            return productList;
        }
        public async Task<Product> GetProductById(int id)
        {
            var product = await db.Product.FindAsync(id);
            return product;
        }
        public async Task<Product> Addproduct(Product product)
        {
            await db.Product.AddAsync(product);
            await db.SaveChangesAsync();
            return product;
        }
        public async Task<Product> UpdateProduct(Product product, int id)
        {
            var model = await db.Product.FindAsync(id);
            if (model != null)
            {
                model.Product_Name = product.Product_Name;
                model.Product_Description = product.Product_Description;
                model.Product_rate = product.Product_rate;
                await db.SaveChangesAsync();
            }
            return model;
        }

        public async Task DeleteProduct(int id)
        {
            var product = await db.Product.FindAsync(id);
            if (product != null)
            {
                db.Product.Remove(product);
                await db.SaveChangesAsync();
            }
        }

        public async Task<bool> ProductExists(int id)
        {
            return await db.Product.CountAsync(x => x.Product_Id == id) > 0;
        }



    }
}
