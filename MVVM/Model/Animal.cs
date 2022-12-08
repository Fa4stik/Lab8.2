using System;
using System.Collections.Generic;

namespace PIS8_2.MVVM.Model;

public partial class Animal
{
    public int Id { get; set; }

    public string Kingcolor { get; set; }

    public string Ears { get; set; }

    public string Tail { get; set; }

    public string Description { get; set; }

    public virtual ICollection<Card> Cards { get; } = new List<Card>();
}
