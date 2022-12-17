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
                

                var cards= db.Cards
                    .Include(c => c.IdOrgNavigation)
                    .Include(c => c.IdMunicipNavigation)
                    .Include(c => c.IdOmsuNavigation)
                    .Where(c => c.IdOrg == user.IdOrg)
                    .ToList()
                    //.Where(c=>c.AccessRoles.Contains(user.Role))
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

        public void EditCard(Card newCard)
        {
            using (var db = new TrappinganimalsContext())
            {
                db.Cards.Update(newCard);
                db.SaveChanges();
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

        public void AddCard(Card card)
        {
            using (var db = new TrappinganimalsContext())
            {
                db.Cards.Add(card);
                db.SaveChanges();
            }
        }

        public void DeleteCards(LimitedCard[] limitedCards)
        {
            using (var db = new TrappinganimalsContext())
            {
                foreach (var limitedCard in limitedCards)
                    db.Cards.Remove(db.Cards.FirstOrDefault(c => c.Id == limitedCard.Id));
                db.SaveChanges();
            }
        }
    }
}
