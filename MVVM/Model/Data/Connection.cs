using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS8_2.MVVM.Model.Data
{
    internal class Connection
    {
        //достаем из бд юзера или return null 
        public TUser ExecuteUser(string login, string password)
        {
            return new TUser();
        }
    }
}
