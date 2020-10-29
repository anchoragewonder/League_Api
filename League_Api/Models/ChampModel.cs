using System;
using System.Collections.Generic;
using System.Text;

namespace League_Api.Models
{
    class ChampModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string Class { get; set; }
        public int Style { get; set; }
        public int Difficulty { get; set; }
        public string DamageType { get; set; }
        public int Damage { get; set; }
        public int Sturdiness { get; set; }
        public int CrowdControl { get; set; }
        public int Mobility { get; set; }
        public int Functionality { get; set; }

    }
}
