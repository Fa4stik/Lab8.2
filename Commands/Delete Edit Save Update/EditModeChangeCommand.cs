using PIS8_2.Commands.Base;
using PIS8_2.MVVM.ViewModels;


namespace PIS8_2.Commands
{
    internal class EditModeChangeCommand : Command
    {
        private ScheduleTypeViewModel _scheduleTypeViewModel;
        public EditModeChangeCommand(ScheduleTypeViewModel scheduleTypeViewModel)
        {
            _scheduleTypeViewModel = scheduleTypeViewModel;
        }
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            _scheduleTypeViewModel.ChangeEditMode();
        }
    }
}
