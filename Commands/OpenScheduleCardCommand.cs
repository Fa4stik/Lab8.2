using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OfficeOpenXml;
using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.MVVM.ViewModels;
using PIS8_2.Service;

namespace PIS8_2.Commands
{
    internal class OpenScheduleCardCommand:Command
    {
        private readonly ParameterNavigationService<Card, ScheduleTypeViewModel> _navigationService;
        private readonly Connection _conn;
        


        public OpenScheduleCardCommand(ParameterNavigationService<Card, ScheduleTypeViewModel> navigationService)
        {
            _navigationService = navigationService;
            _conn=new Connection();
        }
        public override bool CanExecute(object parameter) => parameter is LimitedCard;

        public override void Execute(object parameter)
        {
            if (parameter is not LimitedCard limitedCard) return;
            var card = _conn.ExecuteCardId(limitedCard.Id);
            _navigationService.Navigate(card);
        }
    }
}
