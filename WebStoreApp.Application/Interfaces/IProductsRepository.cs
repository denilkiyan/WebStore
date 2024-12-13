using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreApp.Domain.Models;

namespace WebStoreApp.Application.Interfaces
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<Product> GetProductByIdAsync(int id);

        Task AddProductAsync(Product product);

        Task UpdateProductAsync(Product product,string name, decimal price, string description,string category);

        Task DeleteProductAsync(Product product);
    }
}

