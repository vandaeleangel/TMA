using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using TMA.Mobile.Domain.Models;
using TMA.Mobile.Domain.Services.Interfaces;
using Xamarin.Forms;

namespace TMA.Mobile.PageModels
{
    public class ChoreDetailPage :FreshBasePageModel
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
        public Command DeleteCommand { get; set; }
        public ChoreDetailPage(IChoreService choreService)
        {
            _choreService = choreService;
            SaveCommand = new Command(Save);
            CancelCommand = new Command(Cancel);
            DeleteCommand = new Command(Delete);
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            var chore = initData as Chore;
            Name = chore.Name;
        }   
        private void Delete(object obj)
        {
            throw new NotImplementedException();
        }

        private async void Cancel(object obj)
        {
            await CoreMethods.PopPageModel(modal: true);

        }

        private async void Save()
        {
            throw new NotImplementedException();
        }
    }
}
