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
            set => SetField(ref _login, value);
        }

        public string Password
        {
            get => _password;
            set => SetField(ref _password, value);
        }

        // Тестовые команды
        //private RelayCommand _authCommand;

        //public RelayCommand AuthCommand => _authCommand ??= new RelayCommand(DoSomethink); 1 вариант записи
        public RelayCommand AuthCommand { get; }
        private bool CanAuthCommandExecute(object parametr) => true;
        //второй вариант записи
        private void OnAuthCommandExecute(object parametr)
        {
            //essageBox.Show(parametr.ToString());
           CloseWindowCommand.Execute(parametr);
        }

        private RelayCommand<ICloseable> _closeWindowCommand;
        public RelayCommand<ICloseable> CloseWindowCommand => _closeWindowCommand ??= new RelayCommand<ICloseable>(CloseWindow);
        public AuthVM()
        {
            AuthCommand=new RelayCommand(OnAuthCommandExecute,CanAuthCommandExecute);
        }
        private void CloseWindow(ICloseable window)
        {
            window?.Close();
        }
    }
}
