using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreApp.Application.Interfaces;
using WebStoreApp.Application.Services.Auth;
using WebStoreApp.Domain.Models;

namespace WebStoreApp.Application.Services
{
    public class UserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly JwtProvider _jwtProvider;
        public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository, JwtProvider jwtProvider)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;

        }
        public async Task<User> GetByEmail(string email)
        {
            User user = await _userRepository.GetUserByEmailAsync(email);
            return user;
        }
        public async Task Register(string email, string userName, string password)
        {
            string passwordHash = _passwordHasher.Generate(password);
            User user = User.CreateUser(email, userName, passwordHash);
            await _userRepository.AddUserAsync(user);
        }

        public async Task<(bool IsSuccess, string Token, string ErrorMessage)> Login(string email, string password)
        {
            User user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null) return (false, null, "User not found");
            if (!_passwordHasher.Verify(password, user.PasswordHash)) return (false, null, "Password is incorrect");
            else
            {
                var token = _jwtProvider.GenerateToken(user);
                return (true, token, null);
            }
        }

    }
}
