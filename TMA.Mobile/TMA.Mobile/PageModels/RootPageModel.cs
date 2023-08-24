﻿using FreshMvvm;
using Plugin.LocalNotification;
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
        public Command TestCommand { get; set; }


        public RootPageModel()
        {
            ToHomePageCommand = new Command(ToHomePage);
            ToDetailPageCommand = new Command(ToDetailPage);
            ToChorePageCommand = new Command(ToChorePage);
            ToReportPageCommand = new Command(ToReportPage);
            TestCommand = new Command(ScheduleAsync);
        }

        private async void ScheduleAsync(object obj)
        {
            var notification = new NotificationRequest
            {
                BadgeNumber = 1,
                Description = "Test Description",
                Title = "Notification!",
                ReturningData = "Dummy Data",
                NotificationId = 1337,
                Schedule =
                {
                     NotifyTime = DateTime.Now.AddSeconds(5)
                }
            };

            await LocalNotificationCenter.Current.Show(notification);
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
