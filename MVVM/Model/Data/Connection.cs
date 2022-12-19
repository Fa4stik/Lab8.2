﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;
using System.Linq.Expressions;
using System.Security.Cryptography.Xml;
using PIS8_2.Commands;
using PIS8_2.Converters;
using static PIS8_2.MVVM.Model.Tuser;
using System.Security.RightsManagement;
using PIS8_2.Stores;
using System.Reflection;
using System.Windows;
using Microsoft.Extensions.Logging;
using System.Data.Entity.Infrastructure.Interception;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Win32;
using PIS8_2.MVVM.ViewModels;
using System.Drawing;
using System.IO;

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

        //public IEnumerable<Card> ExecuteCards(Tuser user)
        //{
        //    //поправить
        //    //var id = user.IdOrg ?? GetMunicip(user.IdOmsu);
        //    using (var db = new TrappinganimalsContext())
        //    {
               
        //        return db.Cards
        //            .Include(c => c.IdOrgNavigation)
        //            .Include(c => c.IdMunicipNavigation)
        //            .Include(c => c.IdOmsuNavigation)
        //            .Where(c => c.IdOrg == user.IdOrg)
        //            .ToList()
        //            //.Where(c=>c.AccessRoles.Contains(user.Role))
        //            .ToList();
        //    }

        //}

        public void DeleteFile(Card card)
        {
            using (var db = new TrappinganimalsContext())
            {
                var file = db.Files.FirstOrDefault(f => f.Id == card.IdFile);
                file.Name = null;
                file.File = null;
                db.SaveChanges();
            }
        }

        public string AddFile(Card card)
        {

            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "pdf files (*.pdf)|*.pdf";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            string fileName = null;

            if (openFileDialog.ShowDialog() == true)
            {
                var filePath = openFileDialog.FileName;
                fileName = Path.GetFileName(filePath);
                var file = System.IO.File.ReadAllBytes(filePath);

                using (var db = new TrappinganimalsContext())
                {
                    var newFile = new FilePdf()
                    {
                        Id = (int)card.IdFile,
                        Name = fileName,
                        File = file,
                    };
                    db.Files.Update(newFile);
                    db.SaveChanges();
                }
            }
            return fileName == null ? "Файл не найден" : fileName;
            // Принимать ид карточки, либо ид файла.
            // Из айди карточки достаём ид файла.
            // Потом мы достём из db.Files с этим айди, меняем в нём Name и File и Update db по этому файлу
        }


        public IEnumerable<Card> ExecuteCardsWithFilter(Tuser user, FilterModel filter=null,List<Sorter> sorterParams=null)
        {
            using (var db = new TrappinganimalsContext())
            {
                var cards = db.Cards
                    .Include(c => c.IdOrgNavigation)
                    .Include(c => c.IdMunicipNavigation)
                    .Include(c => c.IdOmsuNavigation)
                    .Include(c => c.IdFileNavigation)
                    .Where(c => c.IdOrg == user.IdOrg)
                    .Where(c => c.AccessRoles.ToList().Contains(user.Role));
                if (filter != null)
                {
                    cards = cards
                        .Where(c => c.Nummk >= filter.StartNummk && c.Nummk <= filter.EndNummk)
                        .Where(c => c.Datemk >= filter.StartDatemk && c.Datemk <= filter.EndDatemk)
                        .Where(c => EF.Functions.ILike(c.IdMunicipNavigation.Namemunicip,
                            $"%{filter.StartMunicipName}%"))
                        .Where(c => EF.Functions.ILike(c.IdOmsuNavigation.Nameomsu, $"%{filter.StartOmsuName}%"))
                        .Where(c => EF.Functions.ILike(c.IdOrgNavigation.Nameorg, $"%{filter.StartOrgName}%"))
                        .Where(c => EF.Functions.ILike(c.Locality, $"%{filter.StartLocality}%"))
                        .Where(c => c.Numworkorder >= filter.StartNumworkorder &&
                                    c.Numworkorder <= filter.EndNumworkorder)
                        .Where(c => c.Dateworkorder >= filter.StartDateworkorder &&
                                    c.Dateworkorder <= filter.EndDateworkorder)
                        .Where(c => c.Datetrapping >= filter.StartDatetrapping &&
                                    c.Datetrapping <= filter.EndDatetrapping)
                        .Where(c => EF.Functions.ILike(c.TypeOrder.ToString(), $"%{filter.StartTypeOrder}%"))
                        .Where(c => EF.Functions.ILike(c.Targetorder, $"%{filter.StartTargetorder}%"));

                }




                var sorteredCards = (IOrderedQueryable<Card>)cards;
                if (sorterParams != null && sorterParams.All(x => x.NumberSorting == 0)) return sorteredCards.ToList();
                {
                    sorteredCards = sorteredCards.OrderBy(x => 0);

                    foreach (var sorter in sorterParams.OrderBy(c => c.NumberSorting))
                    {
                        sorteredCards = sorter.Direction switch
                        {
                            Direction.Ascending => sorteredCards.ThenBy(sorter.PropetryName),
                            Direction.Descending => sorteredCards.ThenByDescending(sorter.PropetryName),
                            _ => sorteredCards
                        };
                    }
                }


                return sorteredCards.ToList();
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
                    Id = db.Logs.Max(l => l.Id) +1,
                    UserLogin = user.Login,
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
                    .Include(c => c.IdFileNavigation)
                    .FirstOrDefault(c => c.Id == id);
            }
        }

        public void EditCard(Card newCard, Tuser user)
        {
            using (var db = new TrappinganimalsContext())
            {
                db.Cards.Update(newCard);
                db.SaveChanges();
                
            }
            Journaling(user, newCard, Log.operation.editCard);
        }

        public void AddCard(Card _card, Tuser _user)
        {
            using (var db = new TrappinganimalsContext())
            {
                //db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
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

                var id = db.Files
                    .OrderBy(c => c.Id)
                    .LastOrDefault().Id +1;

                _card.IdFile = id;

                db.Files.Add(new FilePdf
                {
                    Id = id,
                    Name = null,
                    File = null,
                });

                db.Cards.Add(_card);
                db.SaveChanges();

                
            }
            Journaling(_user, _card, Log.operation.addCardReestr);
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


//Вынести куда-то
public static class IQueryableExtensions
{
    public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string propertyName, IComparer<object> comparer = null)
    {
        return CallOrderedQueryable(query, "OrderBy", propertyName, comparer);
    }

    public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string propertyName, IComparer<object> comparer = null)
    {
        return CallOrderedQueryable(query, "OrderByDescending", propertyName, comparer);
    }

    public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> query, string propertyName, IComparer<object> comparer = null)
    {
        return CallOrderedQueryable(query, "ThenBy", propertyName, comparer);
    }

    public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> query, string propertyName, IComparer<object> comparer = null)
    {
        return CallOrderedQueryable(query, "ThenByDescending", propertyName, comparer);
    }

    /// <summary>
    /// Builds the Queryable functions using a TSource property name.
    /// </summary>
    public static IOrderedQueryable<T> CallOrderedQueryable<T>(this IQueryable<T> query, string methodName, string propertyName,
            IComparer<object> comparer = null)
    {
        var param = Expression.Parameter(typeof(T), "x");

        var body = propertyName.Replace("DOT",".").Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);

        return comparer != null
            ? (IOrderedQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    methodName,
                    new[] { typeof(T), body.Type },
                    query.Expression,
                    Expression.Lambda(body, param),
                    Expression.Constant(comparer)
                )
            )
            : (IOrderedQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    methodName,
                    new[] { typeof(T), body.Type },
                    query.Expression,
                    Expression.Lambda(body, param)
                )
            );
    }
}
