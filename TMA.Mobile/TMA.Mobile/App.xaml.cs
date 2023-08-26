using FreshMvvm;
using Plugin.LocalNotification;
using System;
using TMA.Mobile.Domain.Services;
using TMA.Mobile.Domain.Services.Interfaces;
using TMA.Mobile.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace TMA.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            FreshIOC.Container.Register<IAuthService, AuthService>();
            FreshIOC.Container.Register<IAppService, AppService>();
            FreshIOC.Container.Register<IChoreService, ChoreService>();
            FreshIOC.Container.Register<ITimeBlockService, TimeBlockService>();

            var mainPage = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
            var navigationContainer = new FreshNavigationContainer(mainPage);

            //navigationContainer.BarBackgroundColor = Color.FromHex("#5f6d93");
            navigationContainer.BarBackgroundColor = Color.White;
            navigationContainer.BarTextColor = Color.FromHex("#5f6d93");

            MainPage = navigationContainer;
         
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            if (Application.Current.Properties.ContainsKey(Constants.GlobalCurrentTask))
            {

                string task = Application.Current.Properties[Constants.GlobalCurrentTask].ToString();
                var notification = new NotificationRequest
                {
                    Description = $"Taak: {task} is nog bezig!",
                    Title = "Opgelet",
                    Schedule =
                {
                     NotifyTime = DateTime.Now.AddSeconds(5)
                }

                };

                LocalNotificationCenter.Current.Show(notification);
            }

        }

        protected override void OnResume()
        {
        }
    }
}
