using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIS8_2.MVVM.ViewModels;
using PIS8_2.Stores;

namespace PIS8_2.Service
{
    internal class NavigationService
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModel> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<ViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
