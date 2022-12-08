using System;
using System.Collections.Generic;

namespace PIS8_2.MVVM.Model;

public partial class Log
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public int IdUser { get; set; }

    public int IdCard { get; set; }

    public virtual Card IdCardNavigation { get; set; }

    public virtual Tuser IdUserNavigation { get; set; }
}
