using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
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
    public class CartRepository : ICartRepository
    {
        private readonly StoreContext _storeContext;
        public CartRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task CreateCartAsync(Cart cart)
        {
            await _storeContext.Carts.AddAsync(cart);
            await _storeContext.SaveChangesAsync();
        }

        public async Task DeleteCartAsync(Cart cart)
        {
            _storeContext.Carts.Remove(cart);
            await _storeContext.SaveChangesAsync();
        }


        public async Task UpdateCartAsync(Cart cart)
        {
            _storeContext.Carts.Update(cart);
            await _storeContext.SaveChangesAsync();
        }
        public async Task<Cart> GetCartAsync(int userId, int productId)
        {
            return await _storeContext.Carts.FirstOrDefaultAsync(c => c.UserID == userId && c.ProductID == productId);
        }

        public async Task<IEnumerable<Cart>> GetCartByUserIdAsync(int userId)
        {
            var carts = (from c in _storeContext.Carts
                         where c.UserID == userId
                         select c).ToList();
            return carts;
        }

    }
}
