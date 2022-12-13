using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model.ExportExcel;
using PIS8_2.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS8_2.Commands
{
    internal class ExportExcelCommand : Command
    {
        private readonly ReestrViewModel _reestrViewModel;

        public ExportExcelCommand(ReestrViewModel reestrViewModel)
        {
            _reestrViewModel = reestrViewModel;
        }

        public override bool CanExecute(object parameter) => _reestrViewModel.Cards.Count != 0;

        public override void Execute(object parameter)
        {
            var reportExcel = new ExportExcelReestr().GenerateReport(_reestrViewModel.Cards);
            new ExportExcelReestr().SaveToExcel(reportExcel);
        }
    }
}
