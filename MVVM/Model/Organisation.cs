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
        public string Name { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<Tuser> Tusers { get; set; }
    }
}
