using FreshMvvm;
using System;
using TMA.Mobile.Domain.Services;
using TMA.Mobile.Domain.Services.Interfaces;
using TMA.Mobile.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TMA.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            FreshIOC.Container.Register<IAuthService, AuthService>();
           
            var mainPage = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
            var navigationContainer = new FreshNavigationContainer(mainPage);

            MainPage = navigationContainer;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
