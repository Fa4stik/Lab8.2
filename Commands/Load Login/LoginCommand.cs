using System.Windows;
using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.MVVM.ViewModels;
using PIS8_2.Service;
using PIS8_2.Stores;

namespace PIS8_2.Commands
{
    internal class LoginCommand:Command
    {
        private readonly LoginViewModel _viewModel;
        private readonly NavigationService<RegistryViewModel> _navigationService;
        private readonly Connection _conn;
        private readonly UserStore _userStore;


        public LoginCommand(LoginViewModel viewModel, UserStore userStore, NavigationService<RegistryViewModel> navigationService)
        {
            _viewModel = viewModel;
            _navigationService = navigationService;
            _userStore = userStore;
            _conn = new Connection();
        }

        public override bool CanExecute(object parameter) => true;/*_viewModel.Login != null && _viewModel.Password != null && _viewModel.Login != "" && _viewModel.Password != "";*/

        public override void Execute(object parameter)
        {
            //var user = _conn.ExecuteUser(_viewModel.Login, _viewModel.Password);
            var user= _conn.ExecuteUser("operOtl", "operOtl");
            if (user != null)
            {
                _userStore.CurrentUser = user;
                _navigationService.Navigate();
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
