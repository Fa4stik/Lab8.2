using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.MVVM.ViewModels;

namespace PIS8_2.Commands.Load
{
    internal class LoadPdfFileCommand : Command
    {
        private readonly ScheduleTypeViewModel _scheduleTypeViewModel;
        private readonly Connection _conn;

        public LoadPdfFileCommand(ScheduleTypeViewModel scheduleTypeViewModel, Connection conn)
        {
            _scheduleTypeViewModel = scheduleTypeViewModel;
            _conn = conn;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {

        }
    }
}
