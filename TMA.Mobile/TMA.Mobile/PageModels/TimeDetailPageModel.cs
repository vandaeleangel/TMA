using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMA.Mobile.Domain.Dtos.TimeBlock;
using TMA.Mobile.Domain.Models;
using TMA.Mobile.Domain.Services.Interfaces;
using Xamarin.Forms;

namespace TMA.Mobile.PageModels
{
    public class TimeDetailPageModel : FreshBasePageModel
    {
        private IChoreService _choreService;
        private ITimeBlockService _timeBlockService;
        private bool _isInitialSet = true;

        private ObservableCollection<Chore> _chores;

        public ObservableCollection<Chore> Chores
        {
            get { return _chores; }
            set { _chores = value; }
        }
        private Chore _selectedChore;

        public Chore SelectedChore
        {
            get { return _selectedChore; }
            set
            {
                _selectedChore = value;
                RaisePropertyChanged();
            }
        }


        private TimeBlock _timeBlock;

        public TimeBlock TimeBlock
        {
            get { return _timeBlock; }
            set
            {
                _timeBlock = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (value != _startDate)
                {
                    if (!_isInitialSet && value > _endDate)
                    {
                        ShowWarning("Start datum kan niet na de eind datum liggen");

                    }
                    else
                    {
                        _startDate = value;
                        RaisePropertyChanged();
                        CalculateDuration();
                    }

                }
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (value != _endDate)
                {
                    if (!_isInitialSet && value < _startDate)
                    {
                        ShowWarning("Eind datum kan niet voor de start datum liggen");

                    }
                    else
                    {
                        _endDate = value;
                        RaisePropertyChanged();
                        CalculateDuration();
                    }

                }
            }
        }

        private TimeSpan _startTime;
        public TimeSpan StartTime
        {
            get { return _startTime; }
            set
            {
                if (value != _startTime)
                {
                    if (!_isInitialSet && value > _endTime)
                    {
                        ShowWarning("Start tijd kan niet na eind tijd liggen");
                    }
                    else
                    {
                        _startTime = value;
                        RaisePropertyChanged();
                        CalculateDuration();
                    }
                }
            }
        }

        private TimeSpan _endTime;
        public TimeSpan EndTime
        {
            get { return _endTime; }
            set
            {
                if (value != _endTime)
                {
                    if (!_isInitialSet && value < _startTime)
                    {
                        ShowWarning("Eind tijd kan niet voor de start tijd liggen");
                    }
                    else
                    {
                        _endTime = value;
                        RaisePropertyChanged();
                        CalculateDuration();
                    }
                }

            }
        }

        private TimeSpan _duration;

        public TimeSpan Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                RaisePropertyChanged();
            }
        }


        public Command SaveCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command DeleteCommand { get; set; }

        public TimeDetailPageModel(ITimeBlockService timeBlockService, IChoreService choreService)
        {
            _timeBlockService = timeBlockService;
            _choreService = choreService;
            Chores = new ObservableCollection<Chore>();
            SaveCommand = new Command(Save);
            CancelCommand = new Command(Cancel);
            DeleteCommand = new Command(Delete);
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            TimeBlock = initData as TimeBlock;
            FetchChores();

            StartTime = TimeBlock.StartTime.TimeOfDay;
            EndTime = TimeBlock.EndTime.TimeOfDay;
            StartDate = TimeBlock.StartTime.Date;
            EndDate = TimeBlock.EndTime.Date;
            Duration = TimeBlock.Duration;

            SelectedChore = SetCurrentChore();
            _isInitialSet = false;

        }
        private Chore SetCurrentChore()
        {
            var chore = Chores.FirstOrDefault(c => c.Id == TimeBlock.ChoreId);
            return chore;
        }
        private async void FetchChores()
        {
            Chores.Clear();

            var chores = await _choreService.GetAllChores();
            foreach (var chore in chores)
            {
                Chores.Add(chore);
            }
            SelectedChore = chores.FirstOrDefault(c => c.Id == TimeBlock.ChoreId);

        }
        private async void Delete(object obj)
        {
            string result = await _timeBlockService.DeleteTimeBlock(TimeBlock.Id);

            if (result == string.Empty)
            {
                await CoreMethods.DisplayAlert("Waarschuwing", "Tijdslot niet verwijderd", "Ok");
            }
            else
            {
                await CoreMethods.DisplayAlert("Succes", result, "Ok");
                await CoreMethods.PopPageModel(TimeBlock, modal: true);
            }
        }

        private async void Cancel(object obj)
        {
            await CoreMethods.PopPageModel(modal: true);
        }

        private async void Save(object obj)
        {
            UpdatedTimeBlockDto updatedTimeBlockDto = new UpdatedTimeBlockDto
            {
                Id = TimeBlock.Id,
                Duration = EndTime - StartTime,
                StartTime = StartDate + StartTime,
                EndTime = EndDate + EndTime,
                ChoreId = SelectedChore.Id
            };

            var result = await _timeBlockService.UpdateTimeBlock(updatedTimeBlockDto);

            if (result == null)
            {
                await CoreMethods.DisplayAlert("Waarschuwing", "Aanpassen niet gelukt", "Ok");
            }
            else
            {
                await CoreMethods.PopPageModel(result, modal: true);
            }
        }
        private async void ShowWarning(string warning)
        {
            await CoreMethods.DisplayAlert("Waarschuwing", warning, "Ok");
        }

        private void CalculateDuration()
        {
            var start = StartDate + StartTime;
            var end = EndDate + EndTime;

            Duration = end - start; 
        }
    }
}
