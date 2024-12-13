using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreApp.Application.Interfaces;
using WebStoreApp.Domain.Models;
using WebStoreApp.Infrastructure.DBContext;

namespace WebStoreApp.Infrastructure.Repositories
{
    public class ProductRepository : IProductsRepository
    {
        private readonly StoreContext _storeContext;
        public ProductRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var products = await _storeContext.Products.ToListAsync();
            return products;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _storeContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        public async Task AddProductAsync(Product product)
        {
            _storeContext.Products.Add(product);
            await _storeContext.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product, string name, decimal price, string description, string category)
        {
            product.Name = name;
            product.Price = price;
            product.Description = description;
            product.Category = category;
            await _storeContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product) 
        {
            _storeContext.Products.Remove(product);
            await _storeContext.SaveChangesAsync();
        }
    }
}
