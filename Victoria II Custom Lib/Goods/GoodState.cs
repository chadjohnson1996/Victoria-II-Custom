using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib.Goods
{
    public class GoodState
    {
        /// <summary>
        /// list of all the goods
        /// </summary>
        public ConcurrentDictionary<string, Good> Goods { get; } = new ConcurrentDictionary<string, Good>();

        public GoodState(KeyValueNode root)
        {
            var toAdd = Good.BuildGoods(root);

            foreach (var good in toAdd)
            {
                Goods[good.Name] = good;
            }
        }
        /// <summary>
        /// populates a set of costs
        /// </summary>
        /// <param name="node">the node to populate from</param>
        /// <returns>the costs</returns>
        public Dictionary<Good, decimal> PopulateCost(KeyValueNode node)
        {
            throw new NotImplementedException();
        }
    }
}
