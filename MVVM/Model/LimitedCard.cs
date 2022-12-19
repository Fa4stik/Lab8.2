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

        //public virtual Municip IdMunicipNavigation { get; set; }

        //public virtual Omsu IdOmsuNavigation { get; set; }

        //public virtual Organisation IdOrgNavigation { get; set; }
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
        //<DataGridTextColumn Header = "Номер МК" Binding="{Binding Nummk}" IsReadOnly="True" />
        //<DataGridTextColumn Header = "Дата заключения МК" Binding="{Binding Datemk}" IsReadOnly="True"/>
        //<DataGridTextColumn Header = "Мун. обр." Binding="{Binding IdMunicipNavigation.IdMunicipNavigationDOTNamemunicip}"  IsReadOnly="True" />
        //<DataGridTextColumn Header = "ОМСУ" Binding="{Binding IdOmsuNavigation.IdOmsuNavigationDOTNameomsu}"  IsReadOnly="True" />
        //<DataGridTextColumn Header = "Исполнитель МК" Binding="{Binding IdOrgNavigation.IdOrgNavigationDOTNameorg}" IsReadOnly="True" />
        //<DataGridTextColumn Header = "Номер заказ-наряда" Binding="{Binding Numworkorder}" IsReadOnly="True" /> 
        //<DataGridTextColumn Header = "Населённый пункт" Binding="{Binding Locality}" IsReadOnly="True" />
        //<DataGridTextColumn Header = "Дата выдачи заказ-наряда" Binding="{Binding Datetrapping}" IsReadOnly="True" />
        //<DataGridTextColumn Header = "Дата отлова" Binding="{Binding Dateworkorder}" IsReadOnly="True" />
        //<DataGridTextColumn Header = "Цель отлова" Binding="{Binding Targetorder}" IsReadOnly="True" />
        //<DataGridTextColumn Header = "Тип отлова" Binding="{Binding TypeOrder}" IsReadOnly="True" />
    }
}
