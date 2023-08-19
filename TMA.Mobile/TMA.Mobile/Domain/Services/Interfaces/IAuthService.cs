﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMA.Mobile.Domain.Dtos.User;

namespace TMA.Mobile.Domain.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(UserLoginDto userLogin);
    }
}