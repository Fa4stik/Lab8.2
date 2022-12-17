using Microsoft.EntityFrameworkCore;
using PIS8_2.Commands.Base;
using PIS8_2.Converters;
using PIS8_2.MVVM.Model;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.MVVM.ViewModels;
using PIS8_2.Stores;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static PIS8_2.MVVM.Model.Tuser;

namespace PIS8_2.Commands
{
    internal class AddModeChangeCommand : Command
    {
        private readonly Connection _conn;
        private readonly Card _card;
        private readonly UserStore _user;
        private readonly ICommand _openScheduleCardCommand;
        public AddModeChangeCommand(Card card, UserStore user, ICommand openScheduleCardCommand)
        {
            _card = card;
            _conn = new Connection();
            _user = user;
            _openScheduleCardCommand = openScheduleCardCommand;
        }

        public override bool CanExecute(object parameter)
        {
            foreach (var item in _card
                                    .GetType()
                                    .GetProperties()
                                    .Where(c => c.PropertyType == typeof(string))
                                    .ToList())
            {
                if (item.GetValue(_card) == null || (string)item.GetValue(_card) == string.Empty)
                    return false;
            }

            foreach (var item in _card
                                    .GetType()
                                    .GetProperties()
                                    .Where(c => c.PropertyType == typeof(Enum))
                                    .ToList())
            {
                if (item.GetValue(_card) == null || (string)item.GetValue(_card) == string.Empty)
                    return false;
            }

            return true;
        }

        public override void Execute(object parameter)
        {
            if (_card.Dateworkorder > _card.Datemk || _card.Datetrapping > _card.Datemk)
                MessageBox.Show("Дата заказ-наряда / отлов не может быть больше даты заключения МК");
            using (var db = new TrappinganimalsContext())
            {
                var card = db.Cards
                    .Include(c => c.IdMunicipNavigation)
                    .Include(c => c.IdOmsuNavigation)
                    .Include(c => c.IdOrgNavigation);

                _card.Id = card
                    .OrderBy(c => c.Id)
                    .LastOrDefault().Id + 1;

                // Муниципальное образование
                _card.IdMunicip = card.FirstOrDefault(c => c.IdMunicipNavigation.Namemunicip == _card.IdMunicipNavigation.Namemunicip).IdMunicipNavigation.Id;
                _card.IdMunicipNavigation = card.FirstOrDefault(c => c.IdMunicip == _card.IdMunicip).IdMunicipNavigation;

                // ОМСУ
                _card.IdOmsu = card.FirstOrDefault(c => c.IdOmsuNavigation.Nameomsu == _card.IdOmsuNavigation.Nameomsu).IdOmsuNavigation.Id;
                _card.IdOmsuNavigation = card.FirstOrDefault(c => c.IdOmsu == _card.IdOmsu).IdOmsuNavigation;

                // Организация
                _card.IdOrg = _user.CurrentUser.IdOrg.Value;
                _card.IdOrgNavigation = card.FirstOrDefault(c => c.IdOrg == _card.IdOrg).IdOrgNavigation;

                int numWordOrder = card.Select(c => c.Numworkorder).ToList().Max()+1;
                _card.Numworkorder = numWordOrder;
                _card.TypeOrder = Card.order_type.schedule;
                _card.AccessRoles = new role_type[] { _user.CurrentUser.Role };

                //string text = "";
                //foreach (var item in _card.GetType().GetProperties())
                //{
                //    text += item.Name + @" \ " + item.GetValue(_card) + "\n";
                //}
                //MessageBox.Show(text);

                db.Add(_card);
                db.SaveChanges();

                //_openScheduleCardCommand.Execute(ConverterCardsToLimitedCards.ConvertCardToLimitedCard(_card));
            }
        }
    }
}
