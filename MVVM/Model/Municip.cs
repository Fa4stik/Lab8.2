using System;
using System.Collections.Generic;

namespace PIS8_2.MVVM.Model;

public partial class Municip
{
    public int Id { get; set; }

    public string Namemunicip { get; set; }

    public virtual ICollection<Card> Cards { get; } = new List<Card>();

    public virtual ICollection<Omsu> Omsus { get; } = new List<Omsu>();
}
