using PIS8_2.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIS8_2.Service;

namespace PIS8_2.MVVM.ViewModels
{
    internal class MainViewModel:ViewModel
    {
        private readonly NavigationStore _navigationStore;

        public ViewModel CurrentViewModel =>_navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
           OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
