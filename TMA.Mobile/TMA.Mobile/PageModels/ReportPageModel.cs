using FreshMvvm;
using Microcharts;
using SkiaSharp;
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

        private PieChart _pieChart;

        public PieChart PieChart
        {
            get { return _pieChart; }
            set { _pieChart = value; }
        }


        public ObservableCollection<ChartEntry> Entries { get; set; }

        public ReportPageModel(IAppService appService, ITimeBlockService timeBlockService)
        {
            _appService = appService;
            _timeBlockService = timeBlockService;
            Chores = new ObservableCollection<Chore>();
            Entries = new ObservableCollection<ChartEntry>();
           
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            var _selectedDate = DateTime.Now;
            FetchChores(_selectedDate);
            FetchChart();
        }

        private void FetchChart()
        {
           var entry1 = new ChartEntry(212)
           {
               Label = "UWP",
               ValueLabel = "112",
               Color = SKColor.Parse("#2c3e50")
           };
            var entry2 = new ChartEntry(248)
            {
                Label = "Android",
                ValueLabel = "648",
                Color = SKColor.Parse("#77d065")
            };
            var entry3 = new ChartEntry(128)
            {
                Label = "iOS",
                ValueLabel = "428",
                Color = SKColor.Parse("#b455b6")
            };
            var entry4 = new ChartEntry(514)
            {
                Label = "Forms",
                ValueLabel = "214",
                Color = SKColor.Parse("#3498db")
            };

            Entries.Add(entry1);
            Entries.Add(entry2);
            Entries.Add(entry3);
            Entries.Add(entry4);    

            PieChart = new PieChart { Entries= Entries, LabelTextSize = 30f};
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
