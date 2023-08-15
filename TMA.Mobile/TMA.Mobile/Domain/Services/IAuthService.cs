using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMA.Mobile.Domain.Dtos.User;

namespace TMA.Mobile.Domain.Services
{
    public interface IAuthService
    {
        Task<string> Login(UserLoginDto userLogin);
    }
}
