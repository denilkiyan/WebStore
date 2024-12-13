using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Runtime.CompilerServices;
using WebStoreApp.Application.DTOModels;
using WebStoreApp.Application.Interfaces;
using WebStoreApp.Application.Services;
using WebStoreApp.Domain.Models;

namespace WebStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null) return NotFound(new { Message = "Product not exist" });
            else return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO productModel)
        {
            var product = _productService.CreateProduct(productModel.Name, productModel.Price, productModel.Description, productModel.Category);
            await _productService.AddProduct(product);
            return Ok(new { Message = $"Added product: {product}" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDTO productModel)
        {
            var product = await _productService.GetProductById(id);
            if (product == null) return NotFound(new { Message = "Product not found", ProductID = id });
            else
            {
                await _productService.UpdateProduct(product, productModel.Name, productModel.Price, productModel.Description, productModel.Category);
                return Ok(new { Message = $"Changed product: {product}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null) return NotFound(new { Message = "Product not found", ProductID = id });
            else
            {
                await _productService.DeleteProduct(product);
                return Ok(new { Message = "Product deleted" });
            }
        }
    }
}
