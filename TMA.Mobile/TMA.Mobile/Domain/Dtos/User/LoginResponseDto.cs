using System;
using System.Collections.Generic;
using System.Text;

namespace TMA.Mobile.Domain.Dtos.User
{
    public class LoginResponseDto
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
