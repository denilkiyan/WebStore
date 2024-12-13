using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreApp.Domain.Models;

namespace WebStoreApp.Application.Interfaces
{
    public interface ICartRepository
    {
        Task CreateCartAsync(Cart cart);
        Task DeleteCartAsync(Cart cart);
        Task<Cart> GetCartAsync(int userId, int productId);
        Task UpdateCartAsync(Cart cart);
        Task<IEnumerable<Cart>> GetCartByUserIdAsync(int userId);

    }
}
