using System;
using System.Collections.Generic;

namespace PIS8_2.MVVM.Model
{
    public partial class Organisation
    {
        public Organisation()
        {
            Cards = new HashSet<Card>();
            Tusers = new HashSet<Tuser>();
        }

        public int Id { get; set; }
        public string Nameorg { get; set; }
        public string Firstnamedir { get; set; }
        public string Surnamedir { get; set; }
        public string Patronymicdir { get; set; }
        public string Adress { get; set; }
        public string Phonenumber { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<Tuser> Tusers { get; set; }
    }
}
