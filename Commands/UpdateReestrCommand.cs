using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.MVVM.ViewModels;

namespace PIS8_2.Commands
{
    internal class UpdateReestrCommand:Command
    {
        private readonly ReestrViewModel _viewModel;
        private readonly Connection _conn;

        public UpdateReestrCommand(ReestrViewModel viewModel)
        {
            _viewModel = viewModel;
            _conn = new Connection();
        }

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            _viewModel.Cards = _conn.ExecuteCards(_viewModel.User).ToList();
        }
    }
}
