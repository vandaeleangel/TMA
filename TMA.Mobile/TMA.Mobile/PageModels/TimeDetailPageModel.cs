using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            SelectedChore = TimeBlock.Chore;
        }
        private async void FetchChores()
        {
            Chores.Clear();

            var chores = await _choreService.GetAllChores();
            foreach (var chore in chores)
            {
                Chores.Add(chore);
            }
        }
        private void Delete(object obj)
        {
            throw new NotImplementedException();
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
