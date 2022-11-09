﻿using System;
using System.Collections.Generic;

namespace PIS8_2
{
    public partial class Animal
    {
        public Animal()
        {
            Cards = new HashSet<Card>();
        }

        public int Id { get; set; }
        public string Animaltype { get; set; }
        public string Kingcolor { get; set; }
        public int? Size { get; set; }
        public string Hair { get; set; }
        public string Ears { get; set; }
        public string Tail { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
