using PIS8_2.Commands.Base;
using PIS8_2.Converters;
using PIS8_2.MVVM.Model;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.Stores;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PIS8_2.Commands.Add
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
            _conn.AddCard(_card, _user.CurrentUser);
            _openScheduleCardCommand.Execute(ConverterCardsToLimitedCards.ConvertCardToLimitedCard(_card));
        }
    }
}
