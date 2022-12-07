using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore;

namespace PIS8_2.MVVM.Model.Data
{
    public class Connection
    {
        //достаем из бд юзера или return null 
        public TUser ExecuteUser(string login, string password)
        {
            var hashPassword = (password);
            using (var db = new trappinganimalsContext())
            {
                var user = db.Tusers.Include(c=>c.IdOrgNavigation).ToList();
                return user.FirstOrDefault(u=>u.Login==login&&u.Passwordhash==password);
            }
        }

        static string HashPassword(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToHexString(bytes).ToLower();
        }

        public ICollection<Card> ExecuteCards(TUser user)
        {
            //поправить
            //var id = user.IdOrg ?? GetMunicip(user.IdOmsu);
            using (var db = new trappinganimalsContext())
            {
                return db.Cards.Include(c => c.IdOrgNavigation).Where(c => c.IdOrg == user.IdOrg).ToList();
            }

        }

        public int GetMunicip(int idOmsu)
        {
            using (var db = new trappinganimalsContext())
            {
                return db.Municips.First(m => m.Id == idOmsu).Id;
            }
        }
    }
}
