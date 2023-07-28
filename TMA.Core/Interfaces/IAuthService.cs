using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMA.SharedDtos.Dtos.User;

namespace TMA.Core.Interfaces
{
    public interface IAuthService
    {
        Task<Guid> Register(UserRegisterDto user);
        Task<string> Login(UserLoginDto user);
        Task<bool> UserExists(string email);
    }
}
