using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using PIS8_2.Commands.Base;
using PIS8_2.MVVM.ViewModels;

namespace PIS8_2.Commands
{
    class SortingCommand:Command
    {
        private readonly ReestrViewModel _viewModel;

        public SortingCommand(ReestrViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override bool CanExecute(object parameter)=>true;

        public override void Execute(object parameter)
        {
            _viewModel.Cards = _viewModel.Cards.OrderBy(c => c.TypeOrder).ToList();
        }
    }
}
