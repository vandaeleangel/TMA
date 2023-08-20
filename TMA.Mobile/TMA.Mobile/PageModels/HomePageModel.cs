using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TMA.Mobile.Domain.Dtos.Chore;
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
        private IChoreService _choreService;
        public ObservableCollection<Chore> Chores { get; set; }
        public Command StopCommand { get; set; }
        public Command NewChoreCommand { get; set; }



        #region UI
        private double _listViewOpacity = 1;

        public double ListViewOpacity
        {
            get { return _listViewOpacity; }
            set
            {
                _listViewOpacity = value;
                RaisePropertyChanged();
            }
        }

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

        #endregion

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



        public HomePageModel(IAppService appService, IChoreService choreService)
        {
            _appService = appService;
            _choreService = choreService;
            Chores = new ObservableCollection<Chore>();
            StopCommand = new Command(async () => await StopTimeBlockAsync());
            NewChoreCommand = new Command(async () => await GoToNewChorePageAsync());
        }

      

        public override  void Init(object initData)
        {
            base.Init(initData);
            FetchChores();
            FetchCurrentChore();
            FetchTotalDurationAsync();

        }
        public override void ReverseInit(object returnedData)
        {
            FetchChores();
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

        private async void FetchTotalDurationAsync()
        {
            string total = await _appService.GetTotalDurationForADay(DateTime.Today);
            if (total != null)
            {
                CurrentTotal = total;
            }
        }

        private async void FetchCurrentChore()
        {
            var chore = await _choreService.GetCurrentChore();

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
                ListViewOpacity = 0.3;
                SelectedChore = null;

            }

            else if (CurrentChore != null && CurrentChore != SelectedChore)
            {
                string message = "Je moet eerst je huidige taak stoppen voor je een andere kan starten.";
                await CoreMethods.DisplayAlert("Opgepast", message, "Ok");
            }


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
            ListViewOpacity = 1;

        }

        private async Task GoToNewChorePageAsync()
        {
            await CoreMethods.PushPageModel<NewChorePageModel>(null,true);

        }
    }
}
