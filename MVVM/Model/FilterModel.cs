using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS8_2.MVVM.Model
{
    public class FilterModel
    {
        public bool IsDefaultFilter { get; set; }

        public int StartNummk { get; set; }
        public int EndNummk { get; set; }


        public DateTime StartDatemk { get; set; }
        public DateTime EndDatemk { get; set; }


        public string StartAdresstrapping { get; set; }
        public string EndAdresstrapping { get; set; }

        public string StartMunicipName { get; set; }
        public string EndMunicipName { get; set; }
               
        public string StartOmsuName { get; set; }
        public string EndOmsuName { get; set; }
             
        public string StartOrgName { get; set; }
        public string EndOrgName { get; set; }

        public int StartNumworkorder { get; set; }
        public int EndNumworkorder { get; set; }

        public string StartLocality { get; set; }
        public string EndLocality { get; set; }

        public DateTime StartDateworkorder { get; set; }
        public DateTime EndDateworkorder { get; set; }

        public DateTime StartDatetrapping { get; set; }
        public DateTime EndDatetrapping { get; set; }

        public string StartTargetorder { get; set; }
        public string EndTargetorder { get; set; }

        public FilterModel()
        {
            StateFilterToDefaultState();
        }

        public void StateFilterToDefaultState()
        {
            IsDefaultFilter=true;
            StartNummk = 0;
            EndNummk = int.MaxValue;
            StartDatemk=DateTime.MinValue;
            EndDatemk=DateTime.MaxValue;
            StartAdresstrapping = null;
            EndAdresstrapping = null;
            StartMunicipName = null;
            EndMunicipName = null;
            StartOmsuName = null;
            EndOmsuName = null;
            StartOrgName = null;
            EndOrgName = null;
            StartNumworkorder = 0;
            EndNumworkorder = int.MaxValue;
            StartDateworkorder=DateTime.MinValue;
            EndDateworkorder=DateTime.MaxValue;
            StartDatetrapping= DateTime.MinValue;
            EndDatetrapping= DateTime.MaxValue;
            StartTargetorder=null;
            EndTargetorder=null;
        }
    }
}
