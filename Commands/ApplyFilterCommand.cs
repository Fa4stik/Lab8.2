using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.MVVM.ViewModels;
using PIS8_2.Stores;

namespace PIS8_2.Commands
{
    internal class ApplyFilterCommand:Command
    {
        private readonly Connection _conn;
        private readonly ReestrViewModel _viewModel;
        private readonly UserStore _userStore;

        public ApplyFilterCommand(ReestrViewModel viewModel, UserStore userStore)
        {
            _viewModel = viewModel;
            _userStore = userStore;
        }

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            _conn.ExecuteCardsWithFilter(_userStore.CurrentUser);
        }
    }
}
