using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PIS8_2.MVVM.Model;

public partial class Card
{
    public int Id { get; set; } // +

    public int Nummk { get; set; } // +

    public DateTime Datemk { get; set; } // +

    public int IdOmsu { get; set; } // +

    public int IdMunicip { get; set; } // +

    public string Adresstrapping { get; set; } // +

    public int Numworkorder { get; set; } // +

    public string Locality { get; set; } // +

    public DateTime Dateworkorder { get; set; } // +

    public DateTime Datetrapping { get; set; } // +

    public string Targetorder { get; set; } // +

    public int IdOrg { get; set; } // +

    public string TypeOrder { get; set; } // +

    public string[] AccessRoles { get; set; } 


    public virtual Municip IdMunicipNavigation { get; set; }

    public virtual Omsu IdOmsuNavigation { get; set; }

    public virtual Organisation IdOrgNavigation { get; set; }

    public virtual ICollection<Log> Logs { get; } = new List<Log>();
}
