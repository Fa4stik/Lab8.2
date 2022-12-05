using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIS8_2.MVVM.ViewModels;
using PIS8_2.Stores;

namespace PIS8_2.Service
{
    internal class ParameterNavigationService<TParameter,TViewModel>
    where TViewModel : ViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TParameter, TViewModel> _createViewModel;

        public ParameterNavigationService(NavigationStore navigationStore, Func<TParameter, TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate(TParameter parameter)
        {
            _navigationStore.CurrentViewModel=_createViewModel(parameter);
        }
    }
}
