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



        public async Task<LoginResponse> Login(UserLoginDto user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == user.Email.ToLower());
            if (dbUser == null)
            {
                return new LoginResponse
                {
                    Status = LoginResult.UserNotFound,
                    Message = "User not found."
                };
            }
            else if (!VerifyPasswordHash(user.Password, dbUser.PasswordHash, dbUser.PasswordSalt))
            {
                return new LoginResponse
                {
                    Status = LoginResult.WrongPassword,
                    Message = "Wrong Password"
                };
            }

            return new LoginResponse
            {
                Status = LoginResult.Success,
                Message = CreateToken(dbUser)
            };
        }


        private string CreateToken(User dbUser)
        {
            return "";
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
          using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public async Task<RegistrationResult> Register(UserRegisterDto user)
        {
            if (await UserExists(user.Email))
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
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower()))
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

        public class LoginResponse
        {
            public LoginResult Status { get; set; }
            public string Message { get; set; } = string.Empty;
        }
        public enum LoginResult
        {
            Success,
            UserNotFound,
            WrongPassword
        }

    }
}
