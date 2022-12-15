﻿using System;
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
                if (filter != null && !filter.IsDefaultFilter)
                {
                    cards = cards.Where(c => c.Nummk > filter.StartNummk).ToList();
                }

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
    }
}
