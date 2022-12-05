using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PIS8_2.Commands;
using PIS8_2.Stores;

namespace PIS8_2.MVVM.ViewModels
{
    internal class ReestrViewModel:ViewModel
    {
        

        public ICommand ExitCommand { get; }


        public ReestrViewModel(NavigationStore navigationStore)
        {
            ExitCommand = new ExitCommand(navigationStore);
        }
    }
}
