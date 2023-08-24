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
    public class ReportPageModel : FreshBasePageModel
    {
        private IAppService _appService;
        private ITimeBlockService _timeBlockService;
        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                RaisePropertyChanged();
                UpdateGraph();

            }
        }

        public ObservableCollection<Chore> Chores { get; set; }

        private PieChart _pieChart;

        public PieChart PieChart
        {
            get { return _pieChart; }
            set
            {
                _pieChart = value;
                RaisePropertyChanged();
            }
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
            FetchChart(DateTime.Today);
        }

        private async void FetchChart(DateTime selectedDate)
        {
            Entries.Clear();
            TimeBlockQueryParametersDto query = new TimeBlockQueryParametersDto
            {
                Date = selectedDate
            };
            var entries = await _appService.GetChartData(query);

            foreach (var entry in entries)
            {
                Entries.Add(entry);
            }

            PieChart = new PieChart { Entries = Entries, LabelTextSize = 30f };
        }

        private void UpdateGraph()
        {
            FetchChart(_selectedDate);
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
