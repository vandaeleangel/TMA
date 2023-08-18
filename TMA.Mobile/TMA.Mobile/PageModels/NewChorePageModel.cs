using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using TMA.Mobile.Domain.Dtos.Chore;
using TMA.Mobile.Domain.Services.Interfaces;
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

        public Command SaveCommand { get; set; }
        public Command CancelCommand { get; set; }
        public NewChorePageModel(IChoreService choreService)
        {
            _choreService = choreService;
            SaveCommand = new Command(Save);
            CancelCommand = new Command(Cancel);
        }

        private async void Cancel(object obj)
        {
           await CoreMethods.PopPageModel(modal: true);
        }

        private async void Save(object obj)
        {
            AddChoreDto addChore = new AddChoreDto
            {
                Name = _name
            };

            var chore = await _choreService.AddNewChore(addChore);
            await CoreMethods.PopPageModel(modal: true);
        }
    }
}
