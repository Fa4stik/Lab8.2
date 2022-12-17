using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.MVVM.ViewModels;
using PIS8_2.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS8_2.Commands
{
    internal class SaveModeChangeCommand : Command
    {
        private readonly ScheduleTypeViewModel _scheduleTypeViewModel;
        private readonly UserStore _userStore;
        private readonly Connection _conn;
        public SaveModeChangeCommand(ScheduleTypeViewModel scheduleTypeViewModel, UserStore userStore)
        {
            _conn = new Connection();
            _userStore = userStore;
            _scheduleTypeViewModel = scheduleTypeViewModel;
        }
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            _conn.EditCard(_scheduleTypeViewModel.Card, _userStore.CurrentUser);
            _scheduleTypeViewModel.ChangeSaveMode();
        }
    }
}
