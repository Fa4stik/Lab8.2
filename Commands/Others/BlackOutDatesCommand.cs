using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.MVVM.ViewModels;
using PIS8_2.Stores;

namespace PIS8_2.Commands.Others
{
    internal class BlackOutDatesCommand:Command
    {
        private readonly Connection _conn;
        private readonly UserStore _userStore;
        private readonly ScheduleTypeViewModel _viewModel;

        public BlackOutDatesCommand( UserStore userStore, ScheduleTypeViewModel viewModel)
        {
            _conn = new Connection();
            _userStore = userStore;
            _viewModel = viewModel;
        }

        public override bool CanExecute(object parameter) => _userStore.CurrentUser.IdOrg!=null;

        public override void Execute(object parameter)
        {
            var b = parameter as RoutedEventArgs;
            var datePicker = b.Source as DatePicker;
            var dates = _conn.GetBlackOutDates(_userStore.CurrentUser.IdOrg)!.Where(x => x.Date != _viewModel.Card.Datetrapping);
            foreach (var blackoutDate  in dates)
            {
                datePicker?.BlackoutDates.Add(new CalendarDateRange(blackoutDate));
            }
            
        }
    }
}
