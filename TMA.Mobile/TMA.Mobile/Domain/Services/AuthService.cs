﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMA.Mobile.Domain.Dtos.User;

namespace TMA.Mobile.Domain.Services
{
    public class AuthService : IAuthService
    {
        public Task<LoginResponse> Login(UserLoginDto userLogin)
        {
            LoginResponse response = new LoginResponse();
            response.Status = LoginResult.Success;

            return Task.FromResult(response);
        }
    }
}
