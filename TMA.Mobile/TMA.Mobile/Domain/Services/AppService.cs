using Microcharts;
using Newtonsoft.Json;
using SkiaSharp;
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
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace TMA.Mobile.Domain.Services
{
    public class AppService : IAppService
    {
        protected ApiClient _httpClient;
        private IChoreService _choreService;
        private IColorService _colorService;

        public AppService(IChoreService choreService, IColorService colorService)
        {
            _httpClient = new ApiClient();
            _choreService = choreService;
            _colorService = colorService;
        }

        public async Task<IEnumerable<ChartEntry>> GetChartData(TimeBlockQueryParametersDto param)
        {
            var filteredChores = await GetFilteredChoreByDate(param);

            var chartEnryList = filteredChores
              .Select(async chore =>
              {
                  var color = await _colorService.ConvertStringValue(chore.Color);

                  return new ChartEntry((float)chore.Duration.TotalHours)
                  {
                      Label = chore.Name,
                      ValueLabel = chore.Duration.ToString("h\\:mm"),
                      Color = color,
                      TextColor = color,
                      ValueLabelColor = color
                  };
              });
                
         
            
            if (chartEnryList != null)
            {
                var chartEntries = await Task.WhenAll(chartEnryList);
                return chartEntries;
            }
            else return null;
        }
   
        public async Task<IEnumerable<Chore>> GetFilteredChoreByDate(TimeBlockQueryParametersDto param)
        {
            var chores = await _choreService.GetAllChores();
            var token = await SecureStorage.GetAsync("AuthToken");

            var response = await _httpClient.GetAsync(token, "/TimeBlock/GetFiltered", param);

            if (response.IsSuccessStatusCode)
            {
                var responseAsString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<TimeBlock>>(responseAsString);

                var filteredTimeBlocksChore = result
                    .GroupBy(tb => tb.ChoreId)
                    .Select(group => new Chore
                    {
                        Name = chores.FirstOrDefault(c => c.Id == group.Key)?.Name,
                        Duration = TimeSpan.FromTicks(group.Sum(tb => tb.Duration.Ticks)),
                        Color = chores.FirstOrDefault(c => c.Id == group.Key)?.Color
                    });
  
                return filteredTimeBlocksChore;
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

            var response = await _httpClient.UpdateAsync(token, "/TimeBlock/EndTime", json);

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
