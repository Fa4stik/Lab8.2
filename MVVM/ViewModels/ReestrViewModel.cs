using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PIS8_2.Commands;
using PIS8_2.MVVM.Model;
using PIS8_2.Service;
using PIS8_2.Stores;

namespace PIS8_2.MVVM.ViewModels
{
    internal class ReestrViewModel:ViewModel
    {
        private readonly TUser _user;
        public string Login => _user.Login;


        public ICommand ExitCommand { get; }


        public ReestrViewModel(TUser user,NavigationStore navigationStore)
        {
            _user = user;


            ExitCommand =
                new ExitCommand(new NavigationService<LoginViewModel>(navigationStore,
                    () => new LoginViewModel(navigationStore)));
        }
    }
}
