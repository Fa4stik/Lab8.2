using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PIS8_2.Commands;
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


        public ICommand LoginCommand { get; }

        public LoginViewModel(NavigationStore navigationStore)
        {
            LoginCommand = new LoginCommand(navigationStore);
            //LoginCommand = new NavigateCommand<ReestrViewModel>(navigationStore, ()=>new ReestrViewModel(navigationStore));
        }
    }
}
