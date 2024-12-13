using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreApp.Application.Interfaces;
using WebStoreApp.Domain.Models;

namespace WebStoreApp.Application.Services
{
    public class ProductService
    {
        private readonly IProductsRepository _productsRepository;
        public ProductService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public Product CreateProduct(string name, decimal price, string description, string category)
        {
            Product product = new Product();
            product.Name = name;
            product.Price = price;
            product.Description = description;
            product.Category = category;
            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var product = await _productsRepository.GetProductsAsync();
            return product;
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _productsRepository.GetProductByIdAsync(id);
            return product;
        }
        public async Task AddProduct(Product product)
        {
            await _productsRepository.AddProductAsync(product);
        }

        public async Task UpdateProduct(Product product, string name, decimal price, string description, string category)
        {
            await _productsRepository.UpdateProductAsync(product, name, price, description, category);
        }

        public async Task DeleteProduct(Product product)
        {
            await _productsRepository.DeleteProductAsync(product);
        }
    }
}
