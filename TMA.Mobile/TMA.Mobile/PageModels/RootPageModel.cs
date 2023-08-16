using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TMA.Mobile.PageModels
{
    public class RootPageModel :FreshBasePageModel
    {
        public Command ToHomePageCommand { get; set; }
        public Command ToDetailPageCommand { get; set; }

        public RootPageModel()
        {
            ToHomePageCommand = new Command(ToHomePage);
            ToDetailPageCommand = new Command(ToDetailPage);
        }

        private async void ToDetailPage(object obj)
        {
            await CoreMethods.PushPageModel<TimeBlockDetailPageModel>();
        }

        private async void ToHomePage(object obj)
        {
            await CoreMethods.PushPageModel<HomePageModel>();
        }
    }
}
