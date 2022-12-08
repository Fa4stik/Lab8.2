using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.MVVM.ViewModels;
using PIS8_2.Service;
using PIS8_2.Stores;

namespace PIS8_2.Commands
{
    internal class LoginCommand:Command
    {
        private readonly LoginViewModel _viewModel;
        private readonly ParameterNavigationService<Tuser, ReestrViewModel> _navigationService;
        private readonly Connection _conn;


        public LoginCommand(LoginViewModel viewModel, ParameterNavigationService<Tuser, ReestrViewModel> navigationService)
        {
            _viewModel = viewModel;
            _navigationService = navigationService;
            _conn = new Connection();
        }

        public override bool CanExecute(object parameter) => _viewModel.Login != null && _viewModel.Password != null && _viewModel.Login != "" && _viewModel.Password != "";

        public override void Execute(object parameter)
        {
            var user = _conn.ExecuteUser(_viewModel.Login, _viewModel.Password);
            if (user != null)
            {
                //_navigationService.Navigate(user);
            }
            else
            {
                //MessageBox.Show("Неправельный логин или пароль");
                _viewModel.VisabilitiError = Visibility.Visible;
                _viewModel.Error = "Неверный логин или пароль";
            }


        }
    }
}
