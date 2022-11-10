using System;
using System.Collections.Generic;

namespace PIS8_2.Model
{
    public partial class Card
    {
        public Card()
        {
            Logs = new HashSet<Log>();
        }

        public int Id { get; set; }
        public int Nummk { get; set; }
        public DateOnly Datemk { get; set; }
        public string Omsu { get; set; }
        public string Executormk { get; set; }
        public int Numworkorder { get; set; }
        public string Locality { get; set; }
        public DateOnly Dateworkorder { get; set; }
        public string Targetorder { get; set; }
        public int IdOrg { get; set; }
        public int? Animalid { get; set; }

        public virtual Animal Animal { get; set; }
        public virtual Organisation IdOrgNavigation { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
    }
}
