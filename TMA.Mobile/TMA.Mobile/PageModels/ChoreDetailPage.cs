using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using TMA.Mobile.Domain.Dtos.Chore;
using TMA.Mobile.Domain.Models;
using TMA.Mobile.Domain.Services.Interfaces;
using Xamarin.Forms;

namespace TMA.Mobile.PageModels
{
    public class ChoreDetailPage :FreshBasePageModel
    {
        private IChoreService _choreService;

        public Chore _chore;
        public Chore Chore
        {
            get { return _chore; }
            set
            {
                _chore = value;
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
            Chore= initData as Chore;
        }   
        private async void Delete(object obj)
        {
            string result = await _choreService.DeleteChore(Chore.Id);
            if(result == string.Empty)
            {
                await CoreMethods.DisplayAlert("Waarschuwing", "Taak niet verwijderd", "Ok");
            }
            else
            {
                await CoreMethods.DisplayAlert("Succes", result, "Ok");
                await CoreMethods.PopPageModel(Chore,modal: true);
            }
        }

        private async void Cancel(object obj)
        {
            await CoreMethods.PopPageModel(modal: true);

        }

        private async void Save()
        {
            UpdatedChoreDto updateChore = new UpdatedChoreDto
            {
                Id = Chore.Id,
                Name = Chore.Name,
            };

            await _choreService.UpdateChoreName(updateChore);
            await CoreMethods.PopPageModel(Chore, modal: true);
        }
    }
}
