using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TMA.Mobile.Domain.Models;
using TMA.Mobile.Domain.Services.Interfaces;
using Xamarin.Essentials;

namespace TMA.Mobile.PageModels
{
    public class HomePageModel : FreshBasePageModel
    {
        private IAppService _appService;

        public ObservableCollection<Chore> Chores { get; set; }
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

        private string _currentTask;
        public string CurrentTask
        {
            get { return _currentTask; }
            set { _currentTask = value; }
        }

        public HomePageModel(IAppService appService)
        {
            _appService = appService;
        }
    }
}
