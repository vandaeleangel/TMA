using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TMA.Mobile.Domain.Models;
using TMA.Mobile.Domain.Services.Interfaces;
using Xamarin.Essentials;

namespace TMA.Mobile.PageModels
{
    public class HomePageModel : FreshBasePageModel
    {
        private IAppService _appService;
        public ObservableCollection<Chore> Chores { get; set; }

        private Chore _currentChore;
        public Chore CurrentChore
        {
            get { return _currentChore; }
            set
            {
                _currentChore = value;
                RaisePropertyChanged();
            }
        }

        private Chore _selectedChore;
        public Chore SelectedChore
        {
            get { return _selectedChore; }
            set
            {
                _selectedChore = value;
                if(_selectedChore != null) 
                {
                    StartStopTimeBlock();
                }
              
            }
        }

      

        private string _currentTotal;
		public string CurrentTotal
        {
			get { return _currentTotal; }
            set
            {
                _currentTotal = value;
                RaisePropertyChanged();
            }
        }

  
        public HomePageModel(IAppService appService)
        {
            _appService = appService;
            Chores = new ObservableCollection<Chore>();
            FetchChores();
        }

        private async void FetchChores()
        {
            var chores = await _appService.GetAllChores();
            foreach (var chore in chores)
            {
                Chores.Add(chore);
            }
        }

        private void StartStopTimeBlock()
        {
            if (_currentChore == null)
            {
                
            }

        }
    }
}
