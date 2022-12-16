using Microsoft.EntityFrameworkCore;
using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.Stores;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS8_2.Commands
{
    internal class AddModeChangeCommand : Command
    {
        private readonly Connection _conn;
        private readonly Card _card;
        private readonly UserStore _user;
        public AddModeChangeCommand(Card card, UserStore user)
        {
            _card = card;
            _conn = new Connection();
            _user = user;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            using (var db = new TrappinganimalsContext())
            {
                var card = db.Cards
                    .Include(c => c.IdMunicipNavigation)
                    .Include(c => c.IdOmsuNavigation);

                _card.Id = card
                    .OrderBy(c => c.Id)
                    .LastOrDefault().Id + 1;

                //_card.IdMunicip = card.FirstOrDefault(c => c.IdMunicipNavigation.Namemunicip == _card.IdMunicipNavigation.Namemunicip).IdMunicipNavigation.Id;
                //_card.IdOmsu = card.FirstOrDefault(c => c.IdOmsuNavigation.Nameomsu == _card.IdOmsuNavigation.Nameomsu).IdOmsuNavigation.Id;
                _card.IdOrg = _user.CurrentUser.IdOrg.Value;
                //_card.AccessRoles = new string[] { "Куратор ВетСлужбы" };
                db.Add(_card);
                db.SaveChanges();
            }
        }
    }
}
