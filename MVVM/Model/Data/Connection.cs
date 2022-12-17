using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;
using System.Security.Cryptography.Xml;
using PIS8_2.Commands;
using PIS8_2.Converters;
using static PIS8_2.MVVM.Model.Tuser;
using System.Security.RightsManagement;
using PIS8_2.Stores;

namespace PIS8_2.MVVM.Model.Data
{
    public class Connection
    {
        //достаем из бд юзера или return null 
        public Tuser ExecuteUser(string login, string password)
        {
            var hashPassword = HashPassword(password);
            using (var db = new TrappinganimalsContext())
            {
                var user = db.Tusers.Include(c => c.IdOrgNavigation).Include(c=>c.IdOmsuNavigation.IdMunicipNavigation).ToList();
                return user.FirstOrDefault(u => u.Login == login && u.Passwordhash == hashPassword);
            }
        }

        private static string HashPassword(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToHexString(SHA256.HashData(bytes)).ToLower();
        }

        public IEnumerable<Card> ExecuteCards(Tuser user)
        {
            //поправить
            //var id = user.IdOrg ?? GetMunicip(user.IdOmsu);
            using (var db = new TrappinganimalsContext())
            {
               
                return db.Cards
                    .Include(c => c.IdOrgNavigation)
                    .Include(c => c.IdMunicipNavigation)
                    .Include(c => c.IdOmsuNavigation)
                    .Where(c => c.IdOrg == user.IdOrg)
                    .ToList()
                    //.Where(c=>c.AccessRoles.Contains(user.Role))
                    .ToList();
            }

        }

        public IEnumerable<Card> ExecuteCardsWithFilter(Tuser user, FilterModel filter=null)
        {
            using (var db = new TrappinganimalsContext())
            {


                var cards = db.Cards
                    .Include(c => c.IdOrgNavigation)
                    .Include(c => c.IdMunicipNavigation)
                    .Include(c => c.IdOmsuNavigation)
                    .Where(c => c.IdOrg == user.IdOrg)
                    //.Where(c => c.AccessRoles.ToString()!.Contains(user.Role.ToString()))
                    .ToList();
                    
                if (filter != null)
                {
                    cards = cards
                        .Where(c => c.Nummk >= filter.StartNummk && c.Nummk <= filter.EndNummk)
                        .Where(c => c.Datemk >= filter.StartDatemk && c.Datemk <= filter.EndDatemk)
                        .Where(c => c.Adresstrapping.Contains(filter.StartAdresstrapping, StringComparison.InvariantCultureIgnoreCase))
                        .Where(c => c.IdMunicipNavigation.Namemunicip.Contains(filter.StartMunicipName, StringComparison.InvariantCultureIgnoreCase))
                        .Where(c => c.IdOmsuNavigation.Nameomsu.Contains(filter.StartOmsuName, StringComparison.InvariantCultureIgnoreCase))
                        .Where(c => c.IdOrgNavigation.Nameorg.Contains(filter.StartOrgName, StringComparison.InvariantCultureIgnoreCase))
                        .Where(c => c.Locality.Contains(filter.StartLocality, StringComparison.InvariantCultureIgnoreCase))
                        .Where(c => c.Numworkorder >= filter.StartNumworkorder && c.Numworkorder <= filter.EndNumworkorder)
                        .Where(c => c.Dateworkorder >= filter.StartDateworkorder && c.Dateworkorder <= filter.EndDateworkorder)
                        .Where(c => c.Datetrapping >= filter.StartDatetrapping && c.Datetrapping <= filter.EndDatetrapping)
                        .Where(c => c.TypeOrder.ToString().Contains(filter.StartTypeOrder, StringComparison.InvariantCultureIgnoreCase))
                        .Where(c => c.Targetorder.Contains(filter.StartTargetorder, StringComparison.InvariantCultureIgnoreCase))
                     
                        .ToList();
                }
                //IsDefaultFilter = true;
                //StartNummk = 0;+
                //EndNummk = int.MaxValue;+
                //StartDatemk = DateTime.Now.AddYears(-1);+
                //EndDatemk = DateTime.Now.AddYears(1);+
                //StartAdresstrapping = Empty;+
                //StartMunicipName = Empty;+
                //StartOmsuName = Empty;+
                //StartOrgName = Empty;+
                //StartLocality = Empty;+
                //StartNumworkorder = 0;+
                //EndNumworkorder = int.MaxValue;+
                //StartDateworkorder = DateTime.Now.AddYears(-1);+
                //EndDateworkorder = DateTime.Now.AddYears(1);+
                //StartDatetrapping = DateTime.Now.AddYears(-1);
                //EndDatetrapping = DateTime.Now.AddYears(1);
                //StartTargetorder = Empty;
                //StartTypeOrder = Empty;
                return cards;
            }
        }

        public void Journaling(Tuser user, Card card, Log.operation operation)
        {
            using (var db = new TrappinganimalsContext())
            {
                var log = new Log()
                {
                    Date = DateTime.Now,
                    IdCard = card.Id,
                    Id = db.Logs.Select(l=>l.Id).Max(),
                    IdCardNavigation = card,
                    IdUser = user.Id,
                    IdUserNavigation = user,
                    Operation = operation
                };
                db.Add(log);
                db.SaveChanges();
            }
        }

        public int GetMunicip(int idOmsu)
        {
            using (var db = new TrappinganimalsContext())
            {
                return db.Municips.First(m => m.Id == idOmsu).Id;
            }
        }

        public Card ExecuteCardId(int id)
        {
            using (var db = new TrappinganimalsContext())
            {
                return db
                    .Cards
                    .Include(c => c.IdOrgNavigation)
                    .Include(c => c.IdMunicipNavigation)
                    .Include(c => c.IdOmsuNavigation)
                    .FirstOrDefault(c => c.Id == id);
            }
        }

        public void EditCard(Card newCard, Tuser user)
        {
            using (var db = new TrappinganimalsContext())
            {
                db.Cards.Update(newCard);
                db.SaveChanges();
                Journaling(user, newCard, Log.operation.editCard);
            }
        }

        public void AddCard(Card _card, Tuser _user)
        {
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
                _card.IdOrg = _user.IdOrg.Value;
                _card.IdOrgNavigation = card.FirstOrDefault(c => c.IdOrg == _card.IdOrg).IdOrgNavigation;

                int numWordOrder = card.Select(c => c.Numworkorder).ToList().Max() + 1;
                _card.Numworkorder = numWordOrder;
                _card.TypeOrder = Card.order_type.schedule;
                _card.AccessRoles = new role_type[] { _user.Role };

                //string text = "";
                //foreach (var item in _card.GetType().GetProperties())
                //{
                //    text += item.Name + @" \ " + item.GetValue(_card) + "\n";
                //}
                //MessageBox.Show(text);

                db.Add(_card);
                db.SaveChanges();

                Journaling(_user, _card, Log.operation.addCardReestr);
            }
        }

        public List<string> GetNamesMunicip()
        {
            using (var db = new TrappinganimalsContext())
            {
                return db.Municips
                    .Select(m => m.Namemunicip)
                    .ToList();
            }
        }

        public List<string> GetNamesOMSU()
        {
            using (var db = new TrappinganimalsContext())
            {
                return db.Omsus
                    .Select(m => m.Nameomsu)
                    .ToList();
            }
        }

        public void DeleteCards(int[] idCards, Tuser user)
        {
            using (var db = new TrappinganimalsContext())
            {
                foreach (var i in idCards)
                {
                    var card = db.Cards.FirstOrDefault(c => c.Id == i);
                    Journaling(user, card, Log.operation.delCardReestr);
                    db.Cards.Remove(card);
                }
                db.SaveChanges();
            }
        }
    }
}
