using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStoreApp.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public static User CreateUser(string email, string name, string passwordHash)
        {
            User user = new User();
            user.Email = email;
            user.Name = name;
            user.PasswordHash = passwordHash; 
            return user;
        }
    }
}

