using PIS8_2.Commands.Base;
using PIS8_2.MVVM.ViewModels;
using PIS8_2.Service;

namespace PIS8_2.Commands
{
    internal class ExitCommand:Command
    {
        private readonly NavigationService<LoginViewModel> _navigationService;

        public ExitCommand(NavigationService<LoginViewModel> navigationService)
        {
            _navigationService = navigationService;
        }

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        { 
            _navigationService.Navigate();
        }
    }
}
