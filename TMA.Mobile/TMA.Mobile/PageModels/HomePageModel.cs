using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace TMA.Mobile.PageModels
{
    public class HomePageModel : FreshBasePageModel
    {
		private string _token;

		public string Token
		{
			get { return _token; }
            set
            {
                _token = value;
                RaisePropertyChanged();
            }
        }

        public HomePageModel()
        {
            FetchToken();
        }

        private async void FetchToken()
        {
            string authToken = await SecureStorage.GetAsync("AuthToken");
            Token = authToken;
        }
    }
}
