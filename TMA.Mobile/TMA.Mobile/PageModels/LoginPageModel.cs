﻿using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using TMA.Mobile.Domain.Dtos.User;
using TMA.Mobile.Domain.Services.Interfaces;
using Xamarin.Forms;

namespace TMA.Mobile.PageModels
{
    public class LoginPageModel : FreshBasePageModel
    {
        private IAuthService _authService;
        private string _email = "test@hotmail.com";
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged();
            }
        }

        private string _password = "Test123";
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }
        private string _error;
        public string Error
        {
            get { return _error; }
            set
            {
                _error = value;
                RaisePropertyChanged();
            }
        }
        public Command LoginCommand { get; set; }
        public LoginPageModel(IAuthService authService)
        {
            LoginCommand = new Command(SignIn);
            _authService = authService;
        }

        private async void SignIn(object obj)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(Email);
            bool isPasswordEmpty = string.IsNullOrEmpty(Password);

            if (isEmailEmpty || isPasswordEmpty)
            {
                Error = "Beide velden moeten zijn ingevuld.";
            }
            else
            {
                UserLoginDto userLoginDto = new UserLoginDto
                {
                    Email = Email,
                    Password = Password
                };

                LoginResponseDto result = await _authService.Login(userLoginDto);

                if (result.Status == LoginResult.Fail)
                {
                    Error = result.Message;
                }       
                if (result.Status == LoginResult.Success)
                {

                    await CoreMethods.PushPageModel<RootPageModel>();
                }

            }
        }
    }
}
