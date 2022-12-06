using System;
using System.Collections.Generic;

namespace PIS8_2.MVVM.Model
{
    public partial class Municip
    {
        public Municip()
        {
            Omsus = new HashSet<Omsu>();
        }

        public int Id { get; set; }
        public string Namemunicip { get; set; }

        public virtual ICollection<Omsu> Omsus { get; set; }
    }
}