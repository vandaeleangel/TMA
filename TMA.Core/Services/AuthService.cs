using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMA.Core.Data;
using TMA.Core.Entities;
using TMA.Core.Interfaces;
using TMA.SharedDtos.Dtos.User;

namespace TMA.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

      

        public Task<string> Login(UserLoginDto user)
        {
            throw new NotImplementedException();
        }

        public async Task<RegistrationResult> Register(UserRegisterDto user)
        {
            if(await UserExists(user.Email))
            {
                return RegistrationResult.EmailAlreadyExists;
            }

            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User newUser = new User
            {
                Email = user.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Chores = new List<Chore>(),
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return RegistrationResult.Succes;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserExists(string email)
        {
            if(await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }

        public enum RegistrationResult
        {
            Succes,
            EmailAlreadyExists
        }
    }
}
