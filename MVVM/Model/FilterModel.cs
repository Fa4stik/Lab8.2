using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.String;

namespace PIS8_2.MVVM.Model
{
    public class FilterModel
    {
        public int StartNummk { get; set; }
        public int EndNummk { get; set; }


        public DateTime StartDatemk { get; set; }
        public DateTime EndDatemk { get; set; }


        public string StartAdresstrapping { get; set; }


        public string StartMunicipName { get; set; }

        public string StartOmsuName { get; set; }

        public string StartOrgName { get; set; }

        public int StartNumworkorder { get; set; }
        public int EndNumworkorder { get; set; }

        public string StartLocality { get; set; }

        public DateTime StartDateworkorder { get; set; }
        public DateTime EndDateworkorder { get; set; }

        public DateTime StartDatetrapping { get; set; }
        public DateTime EndDatetrapping { get; set; }

        public string StartTargetorder { get; set; }

        public string StartTypeOrder { get; set; }

        public FilterModel()
        {
            StateFilterToDefaultState();
        }

        public void StateFilterToDefaultState()
        {
            StartNummk = 0;
            EndNummk = int.MaxValue;
            StartDatemk=DateTime.Now.AddYears(-3);
            EndDatemk= DateTime.Now.AddYears(1);
            StartAdresstrapping = Empty;
            StartMunicipName = Empty;
            StartOmsuName = Empty;
            StartOrgName = Empty;
            StartLocality= Empty;
            StartNumworkorder = 0;
            EndNumworkorder = int.MaxValue;
            StartDateworkorder= DateTime.Now.AddYears(-3);
            EndDateworkorder= DateTime.Now.AddYears(1);
            StartDatetrapping = DateTime.Now.AddYears(-3);
            EndDatetrapping= DateTime.Now.AddYears(1);
            StartTargetorder = Empty;
            StartTypeOrder= Empty;
           
        }
    }
}
