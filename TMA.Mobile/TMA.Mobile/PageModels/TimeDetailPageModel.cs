using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TMA.Mobile.Domain.Models;
using TMA.Mobile.Domain.Services.Interfaces;
using Xamarin.Forms;

namespace TMA.Mobile.PageModels
{
    public class TimeDetailPageModel : FreshBasePageModel
    {
        private IChoreService _choreService;
        private ITimeBlockService _timeBlockService;

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
                _startDate = value;
                RaisePropertyChanged();
            }
        }
        private TimeSpan _startTime;
        public TimeSpan StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                RaisePropertyChanged();
            }
        }

        private TimeSpan _endTime;
        public TimeSpan EndTime
        {
            get { return _endTime; }
            set
            {
                _endTime = value;
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
            SelectedChore = SetCurrentChore();

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

        private void Save(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
