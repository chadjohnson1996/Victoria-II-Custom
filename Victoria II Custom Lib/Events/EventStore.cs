using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib.Events
{
    public class EventStore
    {
        /// <summary>
        /// the country events
        /// </summary>
        public Dictionary<int, CountryEvent> CountryEvents { get; } = new Dictionary<int, CountryEvent>();

        /// <summary>
        /// the province events
        /// </summary>
        public Dictionary<int, ProvinceEvent> ProvinceEvents { get; } = new Dictionary<int, ProvinceEvent>();
    }
}
