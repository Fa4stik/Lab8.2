using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.MVVM.View;
using PIS8_2.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS8_2.Commands
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
