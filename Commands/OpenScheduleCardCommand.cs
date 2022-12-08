using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model;
using PIS8_2.MVVM.ViewModels;
using PIS8_2.Service;

namespace PIS8_2.Commands
{
    internal class OpenScheduleCardCommand:Command
    {
        private readonly ReestrViewModel _viewModel;
        private readonly ParameterNavigationService<Card, ScheduleTypeViewModel> _navigationService;
        


        public OpenScheduleCardCommand(ReestrViewModel viewModel, ParameterNavigationService<Card, ScheduleTypeViewModel> navigationService)
        {
            _viewModel = viewModel;
            _navigationService = navigationService;
        }
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            _navigationService.Navigate((Card)parameter);
        }
    }
}
