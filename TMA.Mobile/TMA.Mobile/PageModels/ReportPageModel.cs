using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TMA.Mobile.Domain.Dtos.TimeBlock;
using TMA.Mobile.Domain.Models;
using TMA.Mobile.Domain.Services.Interfaces;

namespace TMA.Mobile.PageModels
{
    public class ReportPageModel :FreshBasePageModel
    {
        private IAppService _appService;      
        private ITimeBlockService _timeBlockService;
        public ObservableCollection<Chore> Chores { get; set; }

        public ReportPageModel(IAppService appService, ITimeBlockService timeBlockService)
        {
            _appService = appService;
            _timeBlockService = timeBlockService;
            Chores = new ObservableCollection<Chore>();
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            var _selectedDate = DateTime.Now;
            FetchChores(_selectedDate);
        }

        private async void FetchChores(DateTime selectedDate)
        {
            Chores.Clear();
            TimeBlockQueryParametersDto query = new TimeBlockQueryParametersDto
            {
                Date = DateTime.Today
            };

            var chores = await _appService.GetFilteredChoreByDate(query);
            foreach (var chore in chores)
            {
                Chores.Add(chore);
            }
            

        }
    }
}
