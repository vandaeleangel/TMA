using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMA.Core.Interfaces;
using TMA.SharedDtos.Dtos.User;
using static TMA.Core.Services.AuthService;

namespace TMA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register (UserRegisterDto userRegister)
        {
            var result = await _authService.Register(userRegister);

            switch (result)
            {
                case RegistrationResult.Succes:
                    return Ok("User successfully created");
                case RegistrationResult.EmailAlreadyExists:
                    return Conflict("User with this email already exists");
                default:
                    return BadRequest("Registration failed");
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login (UserLoginDto userLogin)
        {
            var result = await _authService.Login(userLogin);

            switch (result.Status)
            {
                case LoginResult.Fail:
                        return BadRequest(result.Message);
                case LoginResult.Success:
                    return Ok(result.Message);
                default:
                    return BadRequest("Geen geldige inlog poging");
            }
        }

    }
}
