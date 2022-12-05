using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIS8_2.Commands.Base;
using PIS8_2.MVVM.ViewModels;
using PIS8_2.Stores;

namespace PIS8_2.Commands
{
    internal class ExitCommand:Command
    {
        private readonly NavigationStore _navigationStore;

        public ExitCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        { 
            new NavigateCommand<LoginViewModel>(_navigationStore, () => new LoginViewModel(_navigationStore)).Execute(parameter);
        }
    }
}
