using PIS8_2.Commands;
using PIS8_2.Service;
using PIS8_2.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PIS8_2.MVVM.ViewModels
{
    internal class RequestTypeViewModel:ViewModel
    {
        public ICommand BackToReestrCommand { get; }
        public RequestTypeViewModel(NavigationStore navigationStore)
        {
            BackToReestrCommand =
                new BackToReestrCommand(new NavigationService<ReestrViewModel>(navigationStore,
                    () => new ReestrViewModel(navigationStore)));
        }
    }
}
