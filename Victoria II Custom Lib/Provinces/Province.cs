using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria_II_Custom_Lib.Countries;
using Victoria_II_Custom_Lib.Goods;

namespace Victoria_II_Custom_Lib.Provinces
{
    public class Province
    {
        public decimal Consciousness { get; set; }

        public decimal Militancy { get; set; }

        public Country Owner { get; set; }

        public Country Controller { get; set; }

        public Dictionary<string, Country> Cores { get; set; } = new Dictionary<string, Country>();

        public Good TradeGood { get; set; }

        public int LifeRating { get; set; }
    }
}
