using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMA.SharedDtos.Dtos.User;
using static TMA.Core.Services.AuthService;

namespace TMA.Core.Interfaces
{
    public interface IAuthService
    {
        Task<RegistrationResult> Register(UserRegisterDto user);
        Task<LoginResponse> Login(UserLoginDto user);
        Task<bool> UserExists(string email);
    }
}
