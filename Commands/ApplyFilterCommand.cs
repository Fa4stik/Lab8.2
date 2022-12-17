using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.MVVM.ViewModels;
using PIS8_2.Stores;

namespace PIS8_2.Commands
{
    internal class ApplyFilterCommand:Command
    {
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
            if (_viewModel.FilterVisability == Visibility.Collapsed)
            {
                _viewModel.FilterVisability = Visibility.Visible;
                _viewModel.Filter = new FilterModel();
                _viewModel.Filter.StateFilterToDefaultState();
            }
            else
            {
                _viewModel.FilterVisability = Visibility.Collapsed;
                _viewModel.Filter = null;
            }
            
        }
    }
}
