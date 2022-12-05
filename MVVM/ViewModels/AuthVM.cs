using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using PIS8_2.Commands;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.MVVM.Model;

namespace PIS8_2.MVVM.ViewModels
{
    internal class AuthVM:ViewModel
    {
        private Connection connection;



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



        public RelayCommand<ICloseable> AuthCommand { get; }
        private bool CanAuthCommandExecute(object parametr) => true;
        private void OnAuthCommandExecute(ICloseable window)
        {
            if (connection.ExecuteUser(Login, Password)!=null)
            {
                new Reestr().Show();
                CloseWindow(window);
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
           
        }

        private RelayCommand<ICloseable> _closeWindowCommand;
        public RelayCommand<ICloseable> CloseWindowCommand => _closeWindowCommand ??= new RelayCommand<ICloseable>(CloseWindow);
        public AuthVM()
        {
            AuthCommand=new RelayCommand<ICloseable>(OnAuthCommandExecute,CanAuthCommandExecute);


           // _reestrVm = new ReestrVM(this);

            connection=new Connection();
        }

        private void CloseWindow(ICloseable window)
        {
            window?.Close();
        }
    }
}
