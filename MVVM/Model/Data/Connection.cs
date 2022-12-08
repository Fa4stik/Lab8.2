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
        public Tuser ExecuteUser(string login, string password)
        {
            var hashPassword = HashPassword(password);
            using (var db = new TrappinganimalsContext())
            {
                var user = db.Tusers.Include(c => c.IdOrgNavigation).ToList();
                return user.FirstOrDefault(u => u.Login == login && u.Passwordhash == hashPassword);
            }
        }

        static string HashPassword(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToHexString(SHA256.HashData(bytes)).ToLower();
        }

        public ICollection<Card> ExecuteCards(Tuser user)
        {
            //поправить
            //var id = user.IdOrg ?? GetMunicip(user.IdOmsu);
            using (var db = new TrappinganimalsContext())
            {
                return db.Cards.Include(c => c.IdOrgNavigation).Where(c => c.IdOrg == user.IdOrg).ToList();
            }

        }

        public int GetMunicip(int idOmsu)
        {
            using (var db = new TrappinganimalsContext())
            {
                return db.Municips.First(m => m.Id == idOmsu).Id;
            }
        }
    }
}
