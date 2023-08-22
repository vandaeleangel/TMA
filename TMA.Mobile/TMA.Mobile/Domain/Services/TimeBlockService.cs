using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TMA.Mobile.Domain.Dtos.TimeBlock;
using TMA.Mobile.Domain.Models;
using TMA.Mobile.Domain.Services.Interfaces;
using Xamarin.Essentials;

namespace TMA.Mobile.Domain.Services
{
    public class TimeBlockService : ITimeBlockService
    {
        protected ApiClient _httpClient;
        public TimeBlockService()
        {
            _httpClient = new ApiClient();
        }

        public Task<TimeBlock> AddNewTimeBlock(AddTimeBlockDto newTimeBlock)
        {
            throw new NotImplementedException();
        }

        public async Task<string> DeleteTimeBlock(Guid timeBlockId)
        {
            string result = string.Empty;

            var token = await SecureStorage.GetAsync("AuthToken");


            var path = $"/TimeBlock/{timeBlockId}";

            var response = await _httpClient.DeleteAsync(token, path);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return result = "Tijdslot succesvol verwijderd.";
            }
            else return result;
        }

        public async Task<IEnumerable<TimeBlock>> GetFilteredTimeBlocks(TimeBlockQueryParametersDto queryParams)
        {
            var token = await SecureStorage.GetAsync("AuthToken");

            var response = await _httpClient.GetAsync(token, "/TimeBlock/GetFiltered", queryParams);

            if (response.IsSuccessStatusCode)
            {
                var responseAsString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<TimeBlock>>(responseAsString);

                return result.Select(x => new TimeBlock
                {
                    Id = x.Id,
                    ChoreId = x.ChoreId,
                    Duration = x.Duration,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime
                }).ToList();
            }

            return null;

        }

        public Task<string> UpdateTimeBlock(UpdatedTimeBlockDto updatedTimeBlock)
        {
            throw new NotImplementedException();
        }
    }
}
