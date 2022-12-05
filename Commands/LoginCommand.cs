using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model;
using PIS8_2.MVVM.ViewModels;
using PIS8_2.Service;
using PIS8_2.Stores;

namespace PIS8_2.Commands
{
    internal class LoginCommand:Command
    {
        private readonly LoginViewModel _viewModel;
        private readonly ParameterNavigationService<TUser, ReestrViewModel> _navigationService;


        public LoginCommand(LoginViewModel viewModel,ParameterNavigationService<TUser,ReestrViewModel> navigationService)
        {
            _viewModel = viewModel;
            _navigationService = navigationService;
        }

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            //MessageBox.Show(_viewModel.Login);
            //dosomethink
            TUser user = new TUser(_viewModel.Login);
            _navigationService.Navigate(user);
        }
    }
}
