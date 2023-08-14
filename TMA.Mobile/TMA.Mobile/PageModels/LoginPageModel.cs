using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TMA.Mobile.PageModels
{
    public class LoginPageModel : FreshBasePageModel
    {
        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged();
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }
        public Command LoginCommand { get; set; }
        public LoginPageModel()
        {
            LoginCommand = new Command(SignIn);
        }

        private void SignIn(object obj)
        {
           bool isEmailEmpty = string.IsNullOrEmpty(Email);
           bool isPasswordEmpty = string.IsNullOrEmpty(Password);

            if(isEmailEmpty)
            {
                Email = "Je moet een email invullen";
            }

            else if(isPasswordEmpty)
            {
                Password = "Je moet een passwoord invullen";
            }
            else
            {
                Email = "Gelukt";
                Password = "Ook gelukt";
            }
        }
    }
}
