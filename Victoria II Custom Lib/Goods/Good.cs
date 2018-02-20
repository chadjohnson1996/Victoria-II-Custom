using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib.Goods
{
    public class Good
    {
        /// <summary>
        /// list of all the goods
        /// </summary>
        public static ConcurrentDictionary<string, Good> Goods { get; } = new ConcurrentDictionary<string, Good>();

        /// <summary>
        /// populates a set of costs
        /// </summary>
        /// <param name="node">the node to populate from</param>
        /// <returns>the costs</returns>
        public static Dictionary<Good, decimal> PopulateCost(KeyValueNode node)
        {
            throw new NotImplementedException();
        }
    }
}
