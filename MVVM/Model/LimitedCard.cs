using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PIS8_2.MVVM.Model.Card;

namespace PIS8_2.MVVM.Model
{
    public class LimitedCard
    {
        public LimitedCard(int id, int nummk, DateTime datemk, string namemunicip, string nameomsu, int numworkorder, string locality, DateTime dateworkorder, DateTime datetrapping, string targetorder, order_type typeOrder, string nameorg)
        {
            Id = id;
            Nummk = nummk;
            Datemk = datemk;
            Namemunicip = namemunicip;
            Nameomsu = nameomsu;
            Numworkorder = numworkorder;
            Locality = locality;
            Dateworkorder = dateworkorder;
            Datetrapping = datetrapping;
            Targetorder = targetorder;
            TypeOrder = typeOrder;
            Nameorg = nameorg;
        }


        public int Id { get; set; }
        public int Nummk { get; set; }
        public DateTime Datemk { get; set; }

        public string Namemunicip { get; set; }

        public string Nameomsu { get; set; }
        public string Nameorg { get; set; }
        public int Numworkorder { get; set; }

        public string Locality { get; set; }

        public DateTime Dateworkorder { get; set; }
        public DateTime Datetrapping { get; set; }
        public string Targetorder { get; set; }
        public order_type TypeOrder { get; set; }

        

        //<DataGridTextColumn Header = "Номер МК" Binding="{Binding Nummk}" IsReadOnly="True" />
        //<DataGridTextColumn Header = "Дата заключения МК" Binding="{Binding Datemk}" IsReadOnly="True"/>
        //<DataGridTextColumn Header = "Мун. обр." Binding="{Binding IdMunicipNavigation.Namemunicip}"  IsReadOnly="True" />
        //<DataGridTextColumn Header = "ОМСУ" Binding="{Binding IdOmsuNavigation.Nameomsu}"  IsReadOnly="True" />
        //<DataGridTextColumn Header = "Исполнитель МК" Binding="{Binding IdOrgNavigation.Nameorg}" IsReadOnly="True" />
        //<DataGridTextColumn Header = "Номер заказ-наряда" Binding="{Binding Numworkorder}" IsReadOnly="True" /> 
        //<DataGridTextColumn Header = "Населённый пункт" Binding="{Binding Locality}" IsReadOnly="True" />
        //<DataGridTextColumn Header = "Дата выдачи заказ-наряда" Binding="{Binding Datetrapping}" IsReadOnly="True" />
        //<DataGridTextColumn Header = "Дата отлова" Binding="{Binding Dateworkorder}" IsReadOnly="True" />
        //<DataGridTextColumn Header = "Цель отлова" Binding="{Binding Targetorder}" IsReadOnly="True" />
        //<DataGridTextColumn Header = "Тип отлова" Binding="{Binding TypeOrder}" IsReadOnly="True" />
    }
}
