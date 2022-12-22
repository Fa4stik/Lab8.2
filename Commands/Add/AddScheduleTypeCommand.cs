using PIS8_2.Commands.Base;
using PIS8_2.MVVM.ViewModels;
using PIS8_2.Service;

namespace PIS8_2.Commands.Add
{
    internal class AddScheduleTypeCommand : Command
    {
        private readonly NavigationService<ScheduleTypeViewModel> _navigationService;

        public AddScheduleTypeCommand(NavigationService<ScheduleTypeViewModel> navigationService)
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
