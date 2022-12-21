﻿using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model;
using PIS8_2.MVVM.Model.ExportExcel;
using PIS8_2.MVVM.ViewModels;
using PIS8_2.Stores;
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
        private readonly UserStore _userStore;

        public ExportExcelCommand(ReestrViewModel reestrViewModel, UserStore userStore)
        {
            _reestrViewModel = reestrViewModel;
            _userStore = userStore;
        }

        public override bool CanExecute(object parameter) => _reestrViewModel.Cards.Count != 0;

        public override void Execute(object parameter)
        {
            var reportExcel = new ExportExcelReestr().GenerateReport(_userStore.CurrentUser, _reestrViewModel.Filter, _reestrViewModel.SortingList.ToList());
            new ExportExcelReestr().SaveToExcel(reportExcel);
        }
    }
}
