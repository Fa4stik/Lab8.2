using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.MVVM.ViewModels;

namespace PIS8_2.Commands
{
    internal class DeleteFileCommand : Command
    {
        private readonly ScheduleTypeViewModel _scheduleTypeViewModel;
        private readonly Connection _conn;

        public DeleteFileCommand(ScheduleTypeViewModel scheduleTypeViewModel)
        {
            _scheduleTypeViewModel = scheduleTypeViewModel;
            _conn = new Connection();
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            _conn.DeleteFile(_scheduleTypeViewModel.Card);
            _scheduleTypeViewModel.CheckModeDeleteVisibility = System.Windows.Visibility.Collapsed;
            _scheduleTypeViewModel.Card.IdFileNavigation.Name = null;
            _scheduleTypeViewModel.Card = _scheduleTypeViewModel.Card;
        }
    }
}
