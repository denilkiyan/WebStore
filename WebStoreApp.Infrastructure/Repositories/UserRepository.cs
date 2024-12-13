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
    public class UserRepository : IUserRepository
    {
        private readonly StoreContext _storeContext;
        public UserRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task AddUserAsync(User user)
        {
            await _storeContext.Users.AddAsync(user);
            await _storeContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email) 
        {
            User user = await _storeContext.Users.FirstOrDefaultAsync(x=>x.Email==email);
            return user;
        }
    }
}
