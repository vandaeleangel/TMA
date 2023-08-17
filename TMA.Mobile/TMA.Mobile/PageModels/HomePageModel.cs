using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using TMA.Mobile.Domain.Dtos.TimeBlock;
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

        private async void StartStopTimeBlock()
        {
            if (CurrentChore == null)
            {
                AddTimeBlockDto addTimeBlock = new AddTimeBlockDto
                {
                    ChoreId = CurrentChore.Id
                };

                TimeBlock result = await _appService.StartTimeBlock(addTimeBlock);
                CurrentChore.CurrentTimeBlockId = result.Id;
                CurrentChore = SelectedChore;
                CurrentTotal = CurrentChore.Duration.ToString();
            }

            if (CurrentChore != null && CurrentChore != SelectedChore) 
            { 
                throw new NotImplementedException();
            }
            if (CurrentChore != null && CurrentChore == SelectedChore)
            {
                UpdateEndTimeDto updateEndTimeDto = new UpdateEndTimeDto
                {
                    TimeBlockId = CurrentChore.CurrentTimeBlockId
                };

                await _appService.StopTimeBlock(updateEndTimeDto);
                CurrentChore = null;
                SelectedChore = null;
            }

        }
    }
}
