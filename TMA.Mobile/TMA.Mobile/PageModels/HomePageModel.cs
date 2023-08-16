using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace TMA.Mobile.PageModels
{
    public class HomePageModel : FreshBasePageModel
    {
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

    }
}
