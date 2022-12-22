using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PIS8_2.Commands;
using PIS8_2.MVVM.Model;
using PIS8_2.Service;
using PIS8_2.Stores;

namespace PIS8_2.MVVM.ViewModels
{
    internal class LoginViewModel:ViewModel
    {
        private string _login;
        public string Login
        {
            get => _login;
            set => SetField(ref _login, value, "Login"); 
        }

        private string _password;

        public string Password
        {
            get => _password;
            set => SetField(ref _password, value, "Password");
        }

        private string _error;
        public string Error
        {
            get => _error;
            set => SetField(ref _error, value, "Error");
        }

        private Visibility _visabilitiError;
        public Visibility VisabilitiError
        {
            get => _visabilitiError;
            set => SetField(ref _visabilitiError, value, nameof(VisabilitiError));
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel(NavigationStore navigationStore, UserStore userStore)
        {
            #region Commands

            var navigationService =
                new NavigationService<RegistryViewModel>(
                    navigationStore,
                    () => new RegistryViewModel(userStore, navigationStore));
            LoginCommand = new LoginCommand(this, userStore, navigationService);
            

            #endregion
            _visabilitiError = Visibility.Collapsed;
        }
    }
}
