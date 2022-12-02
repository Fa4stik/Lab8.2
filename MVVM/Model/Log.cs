using System;
using System.Collections.Generic;

namespace PIS8_2.MVVM.Model
{
    public  class Log
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int Userid { get; set; }
        public int Cardid { get; set; }

        public virtual Card Card { get; set; }
        public virtual TUser User { get; set; }
    }
}
