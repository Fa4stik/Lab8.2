using Microsoft.EntityFrameworkCore.Internal;
using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model.Export;
using PIS8_2.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS8_2.Commands
{
    internal class ExportWordCommand : Command
    {
        private readonly ScheduleTypeViewModel _scheduleTypeViewModel;

        public ExportWordCommand(ScheduleTypeViewModel scheduleTypeViewModel)
        {
            _scheduleTypeViewModel = scheduleTypeViewModel;
        }

        public override bool CanExecute(object parameter) => !_scheduleTypeViewModel.IsEditMode;

        public override void Execute(object parameter)
        {
            var doc = new ExportWordSchedule().GenerateReport(_scheduleTypeViewModel.Card);
            new ExportWordSchedule().SaveToWord(doc);
        }
    }
}
