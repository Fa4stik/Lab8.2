#nullable enable
using PIS8_2.MVVM.Model;
using System.Collections.Generic;

namespace PIS8_2;

public partial class FilePdf
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public byte[]? File { get; set; }

    public virtual ICollection<Card> Cards { get; } = new List<Card>();
}
