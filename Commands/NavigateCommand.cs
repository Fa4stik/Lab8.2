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
    internal class NavigateCommand<TViewModel>:Command
    where TViewModel : ViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigateCommand(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
