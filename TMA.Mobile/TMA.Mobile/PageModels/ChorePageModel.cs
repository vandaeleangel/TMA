using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TMA.Mobile.Domain.Models;
using TMA.Mobile.Domain.Services.Interfaces;
using Xamarin.Forms;

namespace TMA.Mobile.PageModels
{
    public class ChorePageModel :FreshBasePageModel
    {
        private IChoreService _choreService;
        public ObservableCollection<Chore> Chores { get; set; }
        public Command EditCommand { get; set; }
        public Command DeleteCommand { get; set; }
        private Chore _selectedChore;
        public Chore SelectedChore
        {
            get { return _selectedChore; }
            set
            {
                _selectedChore = value;
                if (_selectedChore != null)
                {
                    GoToChoreDetail();
                }
            }
        }

        public ChorePageModel(IChoreService choreService)
        {
            _choreService = choreService;
            Chores = new ObservableCollection<Chore>();
            
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            FetchChores();
        }
        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
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

        private async Task GoToChoreDetail()
        {
            await CoreMethods.PushPageModel<ChoreDetailPage>(SelectedChore, true);
        }
    }
}
