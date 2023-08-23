using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TMA.Mobile.PageModels
{
    public class RootPageModel :FreshBasePageModel
    {
        public Command ToHomePageCommand { get; set; }
        public Command ToDetailPageCommand { get; set; }
        public Command ToChorePageCommand { get; set; }
        public Command ToReportPageCommand { get; set; }

        public RootPageModel()
        {
            ToHomePageCommand = new Command(ToHomePage);
            ToDetailPageCommand = new Command(ToDetailPage);
            ToChorePageCommand = new Command(ToChorePage);
            ToReportPageCommand = new Command(ToReportPage);
        }

        private async void ToReportPage(object obj)
        {
            await CoreMethods.PushPageModel<ReportPageModel>();
        }

        private async void ToChorePage(object obj)
        {
            await CoreMethods.PushPageModel<ChorePageModel>();
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
