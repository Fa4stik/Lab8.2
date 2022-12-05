using System;
using System.Collections.Generic;

namespace PIS8_2.MVVM.Model
{
    public class TUser
    {
        //public TUser(int id,Role role,int idOrg,int idOmsu,string login,string passwordhash)
        //{
        //    Id=id;
        //    UserRole=role;
        //    IdOrg=idOrg;
        //    IdOmsu=idOmsu;
        //    Login=login;
        //    Passwordhash=passwordhash;
        //}

        public int Id { get; set; }
        public Role UserRole { get; set; }
        public int IdOrg { get; set; }
        public int IdOmsu { get; set; }
        public string Login { get; set; }
        public string Passwordhash { get; set; }

        public enum Role
        {
           
        }
    }
}
