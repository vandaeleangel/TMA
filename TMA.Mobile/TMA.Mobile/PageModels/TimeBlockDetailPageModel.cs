using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TMA.Mobile.Domain.Dtos.TimeBlock;
using TMA.Mobile.Domain.Models;
using TMA.Mobile.Domain.Services.Interfaces;

namespace TMA.Mobile.PageModels
{
    public class TimeBlockDetailPageModel : FreshBasePageModel
    {
        private ITimeBlockService _timeBlockService;
        private DateTime _selectedDate = DateTime.Today;
        public ObservableCollection<TimeBlock> TimeBlocks { get; set; }

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                RaisePropertyChanged();
                UpdateTimeBlocks();

            }
        }

        public TimeBlockDetailPageModel(ITimeBlockService timeBlockService)
        {
            _timeBlockService = timeBlockService;
            TimeBlocks = new ObservableCollection<TimeBlock>();
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            FetchTimeBlocks(_selectedDate);
        }

        private async Task FetchTimeBlocks(DateTime selectedDate)
        {
            TimeBlocks.Clear();
            TimeBlockQueryParametersDto query = new TimeBlockQueryParametersDto
            {
                Date = selectedDate
            };

            var timeBlocks = await _timeBlockService.GetFilteredTimeBlocks(query);
            if(timeBlocks != null)
            {
                foreach (var timeBlock in timeBlocks)
                {
                    TimeBlocks.Add(timeBlock);
                }
            }
           
        }

        private void UpdateTimeBlocks()
        {
            FetchTimeBlocks(_selectedDate);
        }
    }
}
