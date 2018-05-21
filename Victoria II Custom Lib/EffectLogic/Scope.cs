using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria_II_Custom_Lib.Countries;

namespace Victoria_II_Custom_Lib.EffectLogic
{
    public class Scope
    {
        public GameState State { get; set; }

        public Country Country { get; set; }

        public Scope Previous { get; set; }
    }
}
