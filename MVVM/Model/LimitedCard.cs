using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static PIS8_2.MVVM.Model.Card;

namespace PIS8_2.MVVM.Model
{
    public class LimitedCard : INotifyPropertyChanged
    {
        /// <summary>
        /// Конструктор, который принимает ограниченную карточку. Даннный класс необходим для экспорта.
        /// При дальнейшем расширении карточки (добавлении второстепенных полей) экспорт не будет требовать изменений
        /// </summary>
        /// <param name="id">Ид карточки</param>
        /// <param name="nummk">Номер муниципального контракта</param>
        /// <param name="datemk">Дата заключения муниципального контракта</param>
        /// <param name="idMunicipNavigationDotNamemunicip">Навагиация к муниципальному образованию</param>
        /// <param name="idOmsuNavigationDotNameomsu">Навигация к органу местного самоуправления</param>
        /// <param name="numworkorder">Номер заказ-наряда</param>
        /// <param name="locality">Населённый пункт</param>
        /// <param name="dateworkorder">Дата заключения заказ-наряда</param>
        /// <param name="datetrapping">Дата отлова</param>
        /// <param name="targetorder">Цель отлова</param>
        /// <param name="typeOrder">Тип заказ-наряда</param>
        /// <param name="idOrgNavigationDotNameorg">Навигация к организации</param>
        public LimitedCard(int id, int nummk, DateTime datemk, string idMunicipNavigationDotNamemunicip, string idOmsuNavigationDotNameomsu, int numworkorder, string locality, DateTime dateworkorder, DateTime datetrapping, string targetorder, order_type typeOrder, string idOrgNavigationDotNameorg)
        {
            Id = id;
            Nummk = nummk;
            Datemk = datemk;
            IdMunicipNavigationDOTNamemunicip = idMunicipNavigationDotNamemunicip;
            IdOmsuNavigationDOTNameomsu = idOmsuNavigationDotNameomsu;
            Numworkorder = numworkorder;
            Locality = locality;
            Dateworkorder = dateworkorder;
            Datetrapping = datetrapping;
            Targetorder = targetorder;
            TypeOrder = typeOrder;
            IdOrgNavigationDOTNameorg = idOrgNavigationDotNameorg;
        }

        private bool _isSelectedCard;
        public bool IsSelectedCard {
            get => _isSelectedCard;
            set => SetField(ref _isSelectedCard, value, "IsSelectedCard");
        }
        public int Id { get; set; }
        public int Nummk { get; set; }
        public DateTime Datemk { get; set; }
        
        public string IdMunicipNavigationDOTNamemunicip { get; set; }

        public string IdOmsuNavigationDOTNameomsu { get; set; }
        public string IdOrgNavigationDOTNameorg { get; set; }
        public int Numworkorder { get; set; }

        public string Locality { get; set; }

        public DateTime Dateworkorder { get; set; }
        public DateTime Datetrapping { get; set; }
        public string Targetorder { get; set; }
        public order_type TypeOrder { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
