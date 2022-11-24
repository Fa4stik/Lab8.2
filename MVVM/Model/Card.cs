using System;
using System.Collections.Generic;

namespace PIS8_2.MVVM.Model
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
        public int Omsu { get; set; }
        public string Adresstrapping { get; set; }
        public int Numworkorder { get; set; }
        public string Locality { get; set; }
        public DateOnly Dateworkorder { get; set; }
        public DateOnly Datetrapping { get; set; }
        public string Targetorder { get; set; }
        public string Firstnameexecuter { get; set; }
        public string Surnameexecuter { get; set; }
        public string Patronymicexecuter { get; set; }
        public string Phonenumberexecuter { get; set; }
        public string Firstnameappl { get; set; }
        public string Surnameappl { get; set; }
        public string Patronymicappl { get; set; }
        public string Adressappl { get; set; }
        public string Phonenumberappl { get; set; }
        public int IdOrg { get; set; }
        public int? Animalid { get; set; }

        public virtual Animal Animal { get; set; }
        public virtual Organisation IdOrgNavigation { get; set; }
        public virtual Omsu OmsuNavigation { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
    }
}
