using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public async Task<IEnumerable<Chore>> GetAllChores()
        {
            var token = await SecureStorage.GetAsync("AuthToken");

            var response = await _httpClient.GetAsync(token, "/Chore/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var responseAsString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Chore>>(responseAsString);
                return result.Select(x => new Chore
                {
                    Id = x.Id,
                    Name = x.Name,
                    Duration = x.Duration,
                    TimeBlocks = x.TimeBlocks
                }).ToList();
            }

            return null;
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

        public async Task<Chore> GetCurrentChore()
        {
            var token = await SecureStorage.GetAsync("AuthToken");

            var response = await _httpClient.GetAsync(token, "/Chore/Current");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseAsString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Chore>(responseAsString);

                return result;
            }

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return null;
            }

            return null;
        }

        public async Task<string> DeleteChore(Guid choreId)
        {
            string result = string.Empty;

            var token = await SecureStorage.GetAsync("AuthToken");
            var path = $"/Chore/{choreId}";

            var response = await _httpClient.DeleteAsync(token, path);

            if(response.StatusCode == HttpStatusCode.OK)
            {
                return result = "Taak succesvol verwijderd.";
            }
            else return result;
        }

        public async Task<string> UpdateChoreName(UpdatedChoreDto updatedChore)
        {
            var result = string.Empty;

            var token = await SecureStorage.GetAsync("AuthToken");
            var json = JsonConvert.SerializeObject(updatedChore);

            var response = await _httpClient.UpdateAsync(token, "/Chore",json);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseAsString = await response.Content.ReadAsStringAsync();
                Chore chore = JsonConvert.DeserializeObject<Chore>(responseAsString);
                result = chore.Name;
                return result;
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return result;
            }

            return null;
        }

        
    }
}
