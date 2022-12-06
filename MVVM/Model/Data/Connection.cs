using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Security.Policy;

namespace PIS8_2.MVVM.Model.Data
{
    public class Connection
    {
        //достаем из бд юзера или return null 
        public TUser ExecuteUser(string login, string password)
        {
            var hashPassword = HashPassword(password);
            using (var db = new trappinganimalsContext())
            {
                var user = db.Tusers.ToList();
                if (user.Exists(s => s.Login == login && s.Passwordhash == hashPassword))
                    return user.Where(s => s.Login == login).First();
                return null;
            }
        }

        static string HashPassword(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToHexString(bytes).ToLower();
        }
    }
}
