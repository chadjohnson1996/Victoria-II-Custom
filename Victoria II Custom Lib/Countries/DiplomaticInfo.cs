using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib.Countries
{
    public class DiplomaticInfo
    {
        public Dictionary<string, Country> Allies { get; set; }

        public Dictionary<string, Country> Puppets { get; set; }
        
        public Dictionary<string, Country> Spherelings { get; set; }
        public Country Overlord { get; set; }

        public Country SphereLeader { get; set; }

        public decimal Infamy { get; set; }

    }
}
