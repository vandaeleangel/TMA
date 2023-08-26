using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMA.Mobile.Domain.Dtos.Chore;
using TMA.Mobile.Domain.Models;
using TMA.Mobile.Domain.Services.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TMA.Mobile.PageModels
{
    public class NewChorePageModel : FreshBasePageModel
    {
        private IChoreService _choreService;
        public string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set
            {
                _error = value;
                RaisePropertyChanged();
            }
        }

        private bool _isErrorVisible;
        public bool IsErrorVisible
        {
            get { return _isErrorVisible; }
            set
            {
                _isErrorVisible = value;
                RaisePropertyChanged();
            }
        }
        private double _sliderValue;
        public double SliderValue
        {
            get => _sliderValue;
            set
            {
                _sliderValue = value;
                RaisePropertyChanged(nameof(SliderValue));
            }
        }

        public Command SaveCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command SpeakCommand { get; set; }
        public NewChorePageModel(IChoreService choreService)
        {
            _choreService = choreService;
            SaveCommand = new Command(Save);
            CancelCommand = new Command(Cancel);
            SpeakCommand = new Command(Speak);
        }
        private async void Cancel(object obj)
        {
            await CoreMethods.PopPageModel(modal: true);

        }

        private async void Save()
        {
            if(string.IsNullOrEmpty(Name)) 
            {
                IsErrorVisible = true;
                Error = "De naam kan niet leeg zijn.";
            }
            else
            {
                AddChoreDto addChore = new AddChoreDto
                {
                    Name = _name
                };

                var chore = await _choreService.AddNewChore(addChore);               
                await CoreMethods.PopPageModel(addChore, modal: true);
            }

            
        }

        private async void Speak(object obj)
        {
#if __MOBILE__
            ShowAndroidWarning();
#else


            if (Name != null)
            {
                await TextToSpeech.SpeakAsync(Name, new SpeechOptions
                {
                    Volume = (float)SliderValue
                });


#endif
            }
        }

            private async void ShowAndroidWarning()
            {
                await CoreMethods.DisplayAlert("Waarschuwing", "Luisteren op een smartphone is op dit moment niet mogelijk", "Ok");
            }
        }
    }
