using Microsoft.Win32;
using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PIS8_2.Commands
{
    internal class DownloadFileCommand : Command
    {
        private readonly ScheduleTypeViewModel _scheduleTypeViewModel;
        private readonly Connection _conn;

        public DownloadFileCommand (ScheduleTypeViewModel scheduleTypeViewModel)
        {
            _scheduleTypeViewModel = scheduleTypeViewModel;
            _conn = new Connection();
        }

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            _scheduleTypeViewModel.Card.IdFileNavigation.Name = _conn.AddFile(_scheduleTypeViewModel.Card);
            if (string.IsNullOrEmpty(_scheduleTypeViewModel.Card.IdFileNavigation.Name))
                _scheduleTypeViewModel.CheckModeDeleteVisibility = Visibility.Visible;
            _scheduleTypeViewModel.Card = _scheduleTypeViewModel.Card;
        }
    }
}
