using System;
using System.Collections.Generic;

namespace PIS8_2.MVVM.Model
{
    public partial class Tuser
    {
        public Tuser()
        {
            Logs = new HashSet<Log>();
        }

        public int Id { get; set; }
        public int? IdOrg { get; set; }
        public int? IdOmsu { get; set; }
        public string Login { get; set; }
        public string Passwordhash { get; set; }

        public virtual Omsu IdOmsuNavigation { get; set; }
        public virtual Organisation IdOrgNavigation { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
    }
}
