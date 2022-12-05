﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIS8_2.Commands.Base;
using PIS8_2.MVVM.ViewModels;
using PIS8_2.Service;
using PIS8_2.Stores;

namespace PIS8_2.Commands
{
    internal class LoginCommand:Command
    {

        private readonly NavigationStore _navigationStore;

        public LoginCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            //dosomethink
            var navigateCommand = new NavigateCommand<ReestrViewModel>(_navigationStore, () => new ReestrViewModel(_navigationStore));
            navigateCommand.Execute(parameter);
        }
    }
}
