using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS8_2.Commands
{
    internal class SaveModeChangeCommand : Command
    {
        private ScheduleTypeViewModel _scheduleTypeViewModel;
        private Connection _conn;
        public SaveModeChangeCommand(ScheduleTypeViewModel scheduleTypeViewModel)
        {
            _conn = new Connection();
            _scheduleTypeViewModel = scheduleTypeViewModel;
        }
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            _conn.EditCard(_scheduleTypeViewModel.Card);
            _scheduleTypeViewModel.ChangeSaveMode();
        }
    }
}
