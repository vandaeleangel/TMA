using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMA.Mobile.Domain.Dtos.Chore;
using TMA.Mobile.Domain.Models;
using TMA.Mobile.Domain.Services.Interfaces;
using Xamarin.Essentials;

namespace TMA.Mobile.Domain.Services
{
    public class ChoreService : IChoreService
    {
        protected ApiClient _httpClient;

        public ChoreService()
        {
            _httpClient = new ApiClient();
        }

        public async Task<Chore> AddNewChore(AddChoreDto newChore)
        {
            var token = await SecureStorage.GetAsync("AuthToken");
            var json = JsonConvert.SerializeObject(newChore);

            var response = await _httpClient.PostAsync(token, "/Chore", json);

            if (response.IsSuccessStatusCode)
            {
                var responseAsString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Chore>(responseAsString);
                return result;
            }

            return null;
        }
    }
}
