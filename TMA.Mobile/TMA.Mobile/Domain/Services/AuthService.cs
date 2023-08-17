using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TMA.Mobile.Domain.Dtos.User;
using Xamarin.Essentials;

namespace TMA.Mobile.Domain.Services
{   
    public class AuthService : IAuthService
    {
        protected ApiClient _httpClient;
        public AuthService()
        {
            _httpClient = new ApiClient();
        }
        public async Task<LoginResponseDto> Login(UserLoginDto userLogin)
        {
            var json = JsonConvert.SerializeObject(userLogin);
            var response = await _httpClient.PostAsync(string.Empty, "/Auth/Login", json);

            var responseAsString = await response.Content.ReadAsStringAsync();

            LoginResponseDto loginResponse = new LoginResponseDto();
     
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                loginResponse.Status = LoginResult.Fail;
                loginResponse.Message = responseAsString;
            }

            if (response.IsSuccessStatusCode)
            {
                loginResponse.Status = LoginResult.Success;
                await SecureStorage.SetAsync("AuthToken", responseAsString);
            }

            return loginResponse;
        }
    }
}
