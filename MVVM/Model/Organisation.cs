using System;
using System.Collections.Generic;

namespace PIS8_2.MVVM.Model
{
    public  class Organisation
    {
        public Organisation()
        {
            Cards = new HashSet<Card>();
            Tusers = new HashSet<TUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<TUser> Tusers { get; set; }
    }
}
