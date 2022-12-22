using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model.Export;
using PIS8_2.MVVM.ViewModels;

namespace PIS8_2.Commands.Export
{
    internal class ExportWordCommand : Command
    {
        private readonly ScheduleTypeViewModel _scheduleTypeViewModel;

        public ExportWordCommand(ScheduleTypeViewModel scheduleTypeViewModel)
        {
            _scheduleTypeViewModel = scheduleTypeViewModel;
        }

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            var doc = new ExportWordSchedule().GenerateReport(_scheduleTypeViewModel.Card);
            new ExportWordSchedule().SaveToWord(doc);
        }
    }
}
