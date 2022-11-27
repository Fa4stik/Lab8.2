using System;
using System.Collections.Generic;

namespace PIS8_2.MVVM.Model
{
    public partial class Omsu
    {
        public Omsu()
        {
            Cards = new HashSet<Card>();
            Tusers = new HashSet<Tuser>();
        }

        public int Id { get; set; }
        public string Nameomsu { get; set; }
        public int IdMunicip { get; set; }

        public virtual Municip IdMunicipNavigation { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<Tuser> Tusers { get; set; }
    }
}
