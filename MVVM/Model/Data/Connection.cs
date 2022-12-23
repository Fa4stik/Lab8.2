using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using static PIS8_2.MVVM.Model.Tuser;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using Expression = System.Linq.Expressions.Expression;
using PIS8_2.MVVM.ViewModels;
using System.Windows.Controls;

namespace PIS8_2.MVVM.Model.Data
{
    public class Connection
    {
        /// <summary>
        /// Авторизует пользователя по его логину и паролю
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>Возвращает все данные пользователя для отображения реестра. 
        /// Если введён неверный логин / пароль, то возвращает <param name="null"></param></returns>
        public Tuser ExecuteUser(string login, string password)
        {
            var hashPassword = HashPassword(password);
            using (var db = new TrappinganimalsContext())
            {
                var user = db.Tusers.Include(c => c.IdOrgNavigation).Include(c=>c.IdOmsuNavigation.IdMunicipNavigation).ToList();
                return user.FirstOrDefault(u => u.Login == login && u.Passwordhash == hashPassword)!;
            }
        }

        /// <summary>
        /// Хэширует полученный пароль
        /// </summary>
        /// <param name="password">Пароль в стороковом формате</param>
        /// <returns></returns>
        private static string HashPassword(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToHexString(SHA256.HashData(bytes)).ToLower();
        }

        /// <summary>
        /// Удаляет содержимое файла и его название из сущности "Файл"
        /// </summary>
        /// <param name="card">Карточка, из которой необходимо удалить файл</param>
        public void DeleteFile(Card card)
        {
            using (var db = new TrappinganimalsContext())
            {
                var file = db.Files.FirstOrDefault(f => f.Id == card.IdFile);
                file!.Name = null;
                file!.File = null;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Закрепляет файл за выбранной карточкой
        /// </summary>
        /// <param name="idFile">Ид файл, который в карточке</param>
        /// <param name="fileName">Название файл</param>
        /// <param name="file">Содержимое файла</param>
        public void AddFile(int idFile, string fileName, byte[] file)
        {
            using (var db = new TrappinganimalsContext())
            {
                var newFile = new FilePdf
                {
                    Id = idFile,
                    Name = fileName,
                    File = file
                };
                db.Files.Update(newFile);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Сохраняет на компьютер пользователя pdf-файл, который закреплён за карточкой
        /// </summary>
        /// <param name="card">Принимает карточку, для которой необходимо скачать файл</param>
        public void DonwloadFilePdf(Card card)
        {
            var saveDialog = new Microsoft.Win32.SaveFileDialog();
            saveDialog.FileName = $"{card.IdFileNavigation.Name}";
            saveDialog.DefaultExt = ".pdf";
            saveDialog.Filter = "Pdf documents (.pdf)|*.pdf";
            saveDialog.ShowDialog();
            try
            {
                System.IO.File.WriteAllBytes(saveDialog.FileName, card.IdFileNavigation.File);
            }
            catch (Exception)
            {
                MessageBox.Show("Пустой файл нельзя скачать!");
            }
        }

        /// <summary>
        /// Метод сортирует карточки по полученным параметрам
        /// </summary>
        /// <param name="user">Текущий пользователь</param>
        /// <param name="filter">Параметры фильтра</param>
        /// <param name="sorterParams">Параметры сортировки</param>
        /// <returns></returns>
        public List<Card> ExecuteCardsWithFilter(Tuser user, FilterModel filter=null,List<Sorter> sorterParams=null)
        {
            using (var db = new TrappinganimalsContext())
            {
                var cards = db.Cards
                    .Include(c => c.IdOrgNavigation)
                    .Include(c => c.IdMunicipNavigation)
                    .Include(c => c.IdOmsuNavigation)
                    .Include(c => c.IdFileNavigation)
                    .Where(c => c.AccessRoles.ToList().Contains(user.Role));

                if (user.IdOmsu==null)
                {
                    cards = cards.Where(c => c.IdOrg == user.IdOrg);
                }
                else if(user.IdOrg==null)
                {
                    cards = cards.Where(c => c.IdOmsu == user.IdOmsu);
                }
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

        /// <summary>
        /// Добавляет запись в таблицу "Журналирования" БД, 
        /// которая соответсвует дествию пользователя
        /// </summary>
        /// <param name="user">Текущий пользователь</param>
        /// <param name="card">Выбранная / текущая карточка</param>
        /// <param name="operation">Дествие пользователя</param>
        public void Journaling(Tuser user, Card card, Log.operation operation)
        {
            using (var db = new TrappinganimalsContext())
            {
                var log = new Log()
                {
                    Date = DateTime.Now,
                    IdCard = card.Id,
                    Id = db.Logs.Select(c=>c.Id).DefaultIfEmpty().Max() +1,
                    UserLogin = user.Login,
                    Operation = operation
                };
                db.Add(log);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Устанавливает навигацию с внешними ключами и находит первую карточку, 
        /// которая соответсвует полученному id
        /// </summary>
        /// <param name="id">Является ключом в сущности "Карточки"</param>
        /// <returns>Возвращает все данные карточки по полученному id</returns>
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

        /// <summary>
        /// Изменяет полученную карточку, 
        /// в соответствии с полученными данными
        /// </summary>
        /// <param name="newCard">Карточка, которую пользователь изменил</param>
        /// <param name="user">Текущий пользователь</param>
        public void EditCard(Card newCard, Tuser user)
        {
            using (var db = new TrappinganimalsContext())
            {
                db.Cards.Update(newCard);
                db.SaveChanges();
            }
            Journaling(user, newCard, Log.operation.editCard);
        }

        /// <summary>
        /// Создаёт новую карточку, заполняет её введёнными данными 
        /// и добавляет в сущность "Карточки"
        /// </summary>
        /// <param name="_card">Заполненная карточка</param>
        /// <param name="_user">Текущий пользователь</param>
        public void AddCard(Card _card, Tuser _user)
        {
            using (var db = new TrappinganimalsContext())
            {
                var card = db.Cards;
                var omsu = db.Omsus;
                var municips = db.Municips;
                var orgs = db.Organisations;
                _card.Id = card
                    .Select(c => c.Id)
                    .DefaultIfEmpty()
                    .Max() + 1;

                _card.IdMunicip = municips.FirstOrDefault(c => c.Namemunicip == _card.IdMunicipNavigation.Namemunicip)!.Id;
                _card.IdMunicipNavigation = municips.FirstOrDefault(c => c.Id == _card.IdMunicip);

                _card.IdOmsu = omsu.FirstOrDefault(c => c.Nameomsu == _card.IdOmsuNavigation.Nameomsu)!.Id;
                _card.IdOmsuNavigation = omsu.FirstOrDefault(c => c.Id == _card.IdOmsu);

                _card.IdOrg = _user.IdOrg.Value;
                _card.IdOrgNavigation = orgs.FirstOrDefault(c => c.Id == _card.IdOrg);

                int numWordOrder = card.Select(c => c!.Numworkorder!).DefaultIfEmpty().Max()! + 1;
                _card.Numworkorder = numWordOrder;
                _card.TypeOrder = Card.order_type.schedule;
                _card.AccessRoles = new role_type[] { _user.Role };

                var id = db.Files
                    .Select(c => c.Id)
                    .DefaultIfEmpty().Max() +1;

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

        /// <summary>
        /// Обращается к сущности "Муниципальное образование" и 
        /// после проходится по всем названиям
        /// </summary>
        /// <returns>Возвращает названия всех муниципальных образований</returns>
        public List<string> GetNamesMunicip()
        {
            using (var db = new TrappinganimalsContext())
            {
                return db.Municips
                    .Select(m => m.Namemunicip)
                    .ToList();
            }
        }

        /// <summary>
        /// Обращается к сущности "Орган местного самоуправления", 
        /// где проходится по всем названиям
        /// </summary>
        /// <returns>Возвращает список всех названий органа местного самоуправления</returns>
        public List<string> GetNamesOMSU()
        {
            using (var db = new TrappinganimalsContext())
            {
                return db.Omsus
                    .Select(m => m.Nameomsu)
                    .ToList();
            }
        }

        /// <summary>
        /// Обращается к сущности "Файлы", где находит соответсвующий файл
        /// </summary>
        /// <param name="id">Номер выбранного файла</param>
        /// <returns>Возвращает выбранный файл (в виде массива байт)</returns>
        public byte[]? GetFile(int id)
        {
            using (var db = new TrappinganimalsContext())
            {
                return db.Files
                    .Where(c => c.Id == id)
                    .Select(c => c.File)
                    .DefaultIfEmpty()
                    .First();
               
            }
        }

        /// <summary>
        /// Метод ищет даты, на которые уже запланирован отлов. Поиск происходит по карточкам
        /// </summary>
        /// <param name="idOrg">Принимает id организации текущего пользователя</param>
        /// <returns>Возвращает список дат, на которые запланирован отлов</returns>
        public List<DateTime>? GetBlackOutDates(int? idOrg)
        {
            if (idOrg == null) return null;
            using (var db=new TrappinganimalsContext())
            {
                return db.Cards.Where(c => c.IdOrg == idOrg).Select(c => c.Datetrapping).DefaultIfEmpty().ToList();
            }
        }

        /// <summary>
        /// Удаляет карточки из реестра по полученным id
        /// </summary>
        /// <param name="idCards">id удаляемоых карточек</param>
        /// <param name="user">Пользователь, которые совершает удаление</param>
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
