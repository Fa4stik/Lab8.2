﻿using PIS8_2.MVVM.ViewModels;
using PIS8_2.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIS8_2.Commands.Base;

namespace PIS8_2.Commands
{
    internal class BackToReestrCommand:Command
    {
        private readonly NavigationService<ReestrViewModel> _navigationService;

        public BackToReestrCommand(NavigationService<ReestrViewModel> navigationService)
        {
            _navigationService = navigationService;
        }

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}
