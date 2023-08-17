﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            if(response.StatusCode == HttpStatusCode.NoContent)
            {
                return null;
            }

            return null;
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
                    Id= x.Id,
                    ChoreId = x.ChoreId,
                    Duration = x.Duration,
                    StartTime= x.StartTime,
                    EndTime = x.EndTime
                }).ToList();
            }

            return null;

        }

        public async Task<string> GetTotalDurationForADay(DateTime date)
        {
            TimeBlockQueryParametersDto query = new TimeBlockQueryParametersDto
            {
                Date = date
            };

            var result = await GetFilteredTimeBlocks(query);

            TimeSpan totalDuration;
            string totalDurationString;

            if(result != null)
            {
                foreach (var timeBlock in result)
                {
                    totalDuration = totalDuration.Add(timeBlock.Duration);
                }
                totalDurationString = totalDuration.ToString("h'h 'm'm'");
                return totalDurationString;
            }
            else
            {
                totalDurationString = "";
                return totalDurationString;
            }
           
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

        public async Task StopTimeBlock(UpdateEndTimeDto timeBlockDto)
        {
            var token = await SecureStorage.GetAsync("AuthToken");
            var json = JsonConvert.SerializeObject(timeBlockDto);

            var response = await _httpClient.PostAsync(token, "/TimeBlock/EndTime", json);

            if (response.IsSuccessStatusCode)
            {
                var responseAsString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TimeBlock>(responseAsString);

                TimeBlock timeBlock = new TimeBlock
                {
                    Id = result.Id
                };
           }
        }
    }
}
