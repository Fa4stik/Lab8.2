using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.MVVM.ViewModels;
using PIS8_2.Service;
using PIS8_2.Stores;

namespace PIS8_2.Commands
{
    internal class SaveModeChangeCommand : Command
    {
        private readonly ScheduleTypeViewModel _scheduleTypeViewModel;
        private readonly NavigationService<ReestrViewModel> _navigationService;
        private readonly UserStore _userStore;
        private readonly Connection _conn;
        public SaveModeChangeCommand(ScheduleTypeViewModel scheduleTypeViewModel, UserStore userStore, NavigationService<ReestrViewModel> navigationService)
        {
            _conn = new Connection();
            _userStore = userStore;
            _scheduleTypeViewModel = scheduleTypeViewModel;
            _navigationService = navigationService;
        }
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            _conn.EditCard(_scheduleTypeViewModel.Card, _userStore.CurrentUser);
            _scheduleTypeViewModel.ChangeSaveMode();
            _navigationService.Navigate();
        }
    }
}
