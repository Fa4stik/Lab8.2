using PIS8_2.Commands.Base;
using PIS8_2.MVVM.ViewModels;
using PIS8_2.Service;

namespace PIS8_2.Commands.Add
{
    internal class AddRequestTypeCommand : Command
    {
        private readonly NavigationService<RequestTypeViewModel> _navigationService;

        public AddRequestTypeCommand(NavigationService<RequestTypeViewModel> navigationService)
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
