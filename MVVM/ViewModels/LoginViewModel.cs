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
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        private string _error;
        public string Error
        {
            get => _error;
            set
            {
                _error = value;
                OnPropertyChanged(nameof(Error));
            }
        }

        private Visibility _visabilitiError;
        public Visibility VisabilitiError
        {
            get => _visabilitiError;
            set
            {
                _visabilitiError = value;
                OnPropertyChanged(nameof(VisabilitiError));
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel(NavigationStore navigationStore, UserStore userStore)
        {
            #region Commands

            var navigationService =
                new NavigationService<ReestrViewModel>(
                    navigationStore,
                    () => new ReestrViewModel(userStore, navigationStore));
            LoginCommand = new LoginCommand(this, userStore, navigationService);

            #endregion

            _visabilitiError = Visibility.Collapsed;
            //LoginCommand = new NavigateCommand<ReestrViewModel>(navigationStore, ()=>new ReestrViewModel(navigationStore));
        }
    }
}
