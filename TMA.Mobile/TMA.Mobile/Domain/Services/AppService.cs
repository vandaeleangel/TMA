using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMA.Mobile.Domain.Dtos.TimeBlock;
using TMA.Mobile.Domain.Models;
using TMA.Mobile.Domain.Services.Interfaces;
using Xamarin.Essentials;

namespace TMA.Mobile.Domain.Services
{
    public class AppService : IAppService
    {
        protected ApiClient _httpClient;
       
        public AppService()
        {
            _httpClient = new ApiClient(); 
        }
        public async Task<IEnumerable<Chore>> GetAllChores()
        {
            var token = await SecureStorage.GetAsync("AuthToken");

            var response = await _httpClient.GetAsync(token, "/Chore/GetAll");

            if(response.IsSuccessStatusCode)
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

        public async Task<IEnumerable<TimeBlock>> GetFilteredTimeBlocks(TimeBlockQueryParametersDto param)
        {
            //var token = await SecureStorage.GetAsync("AuthToken");
            throw new NotImplementedException();

        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TimeBlock> StartTimeBlock(AddTimeBlockDto timeBlockDto)
        {
            var token = await SecureStorage.GetAsync("AuthToken");
            var json = JsonConvert.SerializeObject(timeBlockDto);

            var response = await _httpClient.PostAsync(token, "/TimeBlock", json);

            if (response.IsSuccessStatusCode)
            {
                var responseAsString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TimeBlock>(responseAsString);

                TimeBlock timeBlock = new TimeBlock
                {
                    Id = result.Id
                };

                return timeBlock;
            }

            return null;
        }

        public Task<TimeBlock> StopTimeBlock(UpdateEndTimeDto timeBlockDto)
        {
            throw new NotImplementedException();
        }
    }
}
