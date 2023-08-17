using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TMA.Mobile.Domain.Dtos.TimeBlock;
using TMA.Mobile.Domain.Models;
using TMA.Mobile.Domain.Services.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TMA.Mobile.PageModels
{
    public class HomePageModel : FreshBasePageModel
    {
        private IAppService _appService;
        public ObservableCollection<Chore> Chores { get; set; }
        public Command StopCommand { get; set; }

        private bool _isStopVisible;
        public bool IsStopVisible
        {
            get { return _isStopVisible; }
            set
            {
                _isStopVisible = value;
                RaisePropertyChanged();
            }
        }
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
                if (_selectedChore != null)
                {
                    StartTimeBlock();
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
            //CurrentChore = new Chore();
            FetchChores();
            StopCommand = new Command(async () => await StopTimeBlockAsync());
        }

        private async Task StopTimeBlockAsync()
        {
            UpdateEndTimeDto updateEndTimeDto = new UpdateEndTimeDto
            {
                TimeBlockId = CurrentChore.CurrentTimeBlockId
            };

            await _appService.StopTimeBlock(updateEndTimeDto);
            CurrentTotal = await _appService.GetTotalDurationForADay(DateTime.Today);
            CurrentChore = null;
            SelectedChore = null;
            IsStopVisible = false;

        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            FetchCurrentChore();
            FetchTotalDurationAsync();

        }

        private async void FetchTotalDurationAsync()
        {
            string total = await _appService.GetTotalDurationForADay(DateTime.Today);
            if(total != null)
            {
                CurrentTotal = total;
            }
        }

        private async void FetchCurrentChore()
        {
            var chore = await _appService.GetCurrentChore();
          
            if (chore != null)
            {
                CurrentChore = chore;
                IsStopVisible = true;
            }
            else
            {
                IsStopVisible = false;
            }
        }

        private async void FetchChores()
        {
            var chores = await _appService.GetAllChores();
            foreach (var chore in chores)
            {
                Chores.Add(chore);
            }
        }

        private async void StartTimeBlock()
        {
            
            if (CurrentChore == null)
            {
                AddTimeBlockDto addTimeBlock = new AddTimeBlockDto
                {
                    ChoreId = SelectedChore.Id
                };

                TimeBlock result = await _appService.StartTimeBlock(addTimeBlock);

                CurrentChore = SelectedChore;
                CurrentChore.CurrentTimeBlockId = result.Id;
                IsStopVisible = true;
                SelectedChore = null;

            }

            else if (CurrentChore != null && CurrentChore != SelectedChore)
            {
                throw new NotImplementedException();
            }


        }
    }
}
