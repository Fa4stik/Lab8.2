using System;
using System.Collections.Generic;

namespace PIS8_2.MVVM.Model;

public partial class Omsu
{
    public int Id { get; set; }

    public string Nameomsu { get; set; }

    public int IdMunicip { get; set; }

    public virtual ICollection<Card> Cards { get; } = new List<Card>();

    public virtual Municip IdMunicipNavigation { get; set; }

    public virtual ICollection<Tuser> Tusers { get; } = new List<Tuser>();
}
