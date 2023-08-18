using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using TMA.Mobile.Domain.Services.Interfaces;
using Xamarin.Forms;

namespace TMA.Mobile.PageModels
{
    public class NewChorePageModel : FreshBasePageModel
    {
        private IAppService _appService;
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
        public NewChorePageModel(IAppService appService)
        {
            _appService = appService;
            SaveCommand = new Command(Save);
            CancelCommand = new Command(Cancel);
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
