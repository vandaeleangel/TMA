using FreshMvvm;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TMA.Mobile.Domain.Dtos.TimeBlock;
using TMA.Mobile.Domain.Models;
using TMA.Mobile.Domain.Services.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TMA.Mobile.PageModels
{
    public class ReportPageModel : FreshBasePageModel
    {
        private IAppService _appService;
        private ITimeBlockService _timeBlockService;

        public Command ScreenshotCommand { get; set; }
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

        private PieChart _pieChartSingle;

        public PieChart PieChartSingle
        {
            get { return _pieChartSingle; }
            set
            {
                _pieChartSingle = value;
                RaisePropertyChanged();
            }
        }

        private PieChart _PieChartAll;
        public PieChart PieChartAll
        {
            get { return _PieChartAll; }
            set
            {
                _PieChartAll = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ChartEntry> SingleEntries { get; set; }
        public ObservableCollection<ChartEntry> AllEntries { get; set; }

        public ReportPageModel(IAppService appService, ITimeBlockService timeBlockService)
        {
            _appService = appService;
            _timeBlockService = timeBlockService;
            ScreenshotCommand = new Command(TakeScreenShot);
            Chores = new ObservableCollection<Chore>();
            SingleEntries = new ObservableCollection<ChartEntry>();
            AllEntries = new ObservableCollection<ChartEntry>();

        }

        private async void TakeScreenShot(object obj)
        {
            var screenshot = await Screenshot.CaptureAsync();

            //var stream = await screenshot.OpenReadAsync();

            //Image = ImageSource.FromStream(() => stream);

        }

        public override void Init(object initData)
        {
            base.Init(initData);
            var _selectedDate = DateTime.Now;
            FetchChores(_selectedDate);
            FetchChart(DateTime.Today);
            FetchChartAll();
        }

        private async void FetchChartAll()
        {
            AllEntries.Clear();
            TimeBlockQueryParametersDto query = new TimeBlockQueryParametersDto();
       
            var entries = await _appService.GetChartData(query);

            foreach (var entry in entries)
            {
                AllEntries.Add(entry);
            }

            PieChartAll = new PieChart { Entries = AllEntries, LabelTextSize = 30f };
        }

        private async void FetchChart(DateTime selectedDate)
        {
            SingleEntries.Clear();
            TimeBlockQueryParametersDto query = new TimeBlockQueryParametersDto
            {
                Date = selectedDate
            };
            var entries = await _appService.GetChartData(query);

            foreach (var entry in entries)
            {
                SingleEntries.Add(entry);
            }

            PieChartSingle = new PieChart { Entries = SingleEntries, LabelTextSize = 30f };
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
