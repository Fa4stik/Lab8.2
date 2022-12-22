using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.MVVM.ViewModels;

namespace PIS8_2.Commands.Load
{
    internal class UploadPdfFileCommand : Command
    {
        private readonly ScheduleTypeViewModel _scheduleTypeViewModel;
        private readonly Connection _conn;

        public UploadPdfFileCommand(ScheduleTypeViewModel scheduleTypeViewModel)
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
            _conn.DonwloadFilePdf(_scheduleTypeViewModel.Card);
        }
    }
}
