using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PIS8_2.Commands;
using PIS8_2.MVVM.Model;
using PIS8_2.MVVM.ViewModels.Base;

namespace PIS8_2.MVVM.ViewModels
{
    class AuthVM:ViewModel
    {
        private string _login;
        private string _password;
        
        public string Login { get =>_login;
            set => SetField(ref _login, value,Login);
        }

        public string Password
        {
            get => _password;
            set => SetField(ref _password, value);
        }


        private ICommand _authCommand;

        public ICommand AuthCommand => _authCommand ?? (_authCommand = new RelayCommand( OnSearch));

        public virtual void OnSearch(object Value = null)
        {
            MessageBox.Show("asdasd");
        }
    }
}
