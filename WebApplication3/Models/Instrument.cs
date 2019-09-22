using System;
using System.Collections.Generic;

namespace WebApplication3.Models
{
    public class Instrument
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Price { get; set; }

        public ICollection<Purchase> Purchases { get; set; }
    }
}