using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Data;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;

namespace Shopbridge_base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly ILogger<ProductsController> logger;
        public ProductsController(IProductService _productService)
        {
            this.productService = _productService;
        }

       
        [HttpGet]   
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            try
            {
                var productList = await productService.GetProducts();
                if(productList.ToList().Count==0)
                {
                    return Ok("No products found");
                }
                return Ok(productList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = await productService.GetProductById(id);

                if (product == null)
                {
                    return NotFound("Product with Id=" + id + " not found");
                }
                return product;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest();

                var Newproduct = await productService.Addproduct(product);

                return CreatedAtAction(nameof(GetProduct), new { id = Newproduct.Product_Id }, Newproduct);
            }
            catch (Exception)
            {
                if (await ProductExists(product.Product_Id))
                {
                    return Conflict();
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new product");
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            try
            {
                var productToUpdate = await productService.GetProductById(id);

                if (productToUpdate == null)
                {
                    return NotFound("Product with Id=" + id + " not found");
                }

                var updateProduct = await productService.UpdateProduct(product, id);

                return Ok("Product with Id=" + id + " updated successfully");

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating database record");
            }
        }

        
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await productService.GetProductById(id);

                if (product == null)
                {
                    return NotFound("Product with Id=" + id + " not found found");
                }

                await productService.DeleteProduct(id);

                return Ok("Product with id=" + id + " deleted");

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting database record");
            }
        }

        private async Task<bool> ProductExists(int id)
        {
            return await productService.ProductExists(id);
        }
    }
}
