using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private IColorService _colorService;
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

        private ObservableCollection<CustomColor> _colors;
        public ObservableCollection<CustomColor> Colors
        {
            get { return _colors; }
            set { _colors = value; }
        }

        private CustomColor _selectedColor;

        public CustomColor SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                _selectedColor = value;
                RaisePropertyChanged();
            }
        }
    
        public Command SaveCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command SpeakCommand { get; set; }
        public NewChorePageModel(IChoreService choreService, IColorService colorService)
        {
            _choreService = choreService;
            _colorService = colorService;
            Colors = new ObservableCollection<CustomColor>();
            SaveCommand = new Command(Save);
            CancelCommand = new Command(Cancel);
            SpeakCommand = new Command(Speak);
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            FetchColors();
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
                    Name = _name,
                    Color = SelectedColor.Name
                    
                };

                var chore = await _choreService.AddNewChore(addChore);               
                await CoreMethods.PopPageModel(addChore, modal: true);
            }
        }

        private async void FetchColors()
        {
            Colors.Clear();

            var colors = await _colorService.GetColors();
            foreach (var color in colors)
            {
                Colors.Add(color);
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
