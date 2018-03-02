using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria_II_Custom_Lib.Extensions;
using Victoria_II_Custom_Lib.General;

namespace Victoria_II_Custom_Lib.Goods
{
    public class Good : LocalizableObject
    {
        static Good()
        {
            GoodTypeLookup = new ConcurrentDictionary<string, GoodTypeEnum>();
            GoodTypeLookup["military_goods"] = GoodTypeEnum.Military;
            GoodTypeLookup["raw_material_goods"] = GoodTypeEnum.RawMaterial;
            GoodTypeLookup["consumer_goods"] = GoodTypeEnum.Consumer;
            GoodTypeLookup["industrial_goods"] = GoodTypeEnum.Industrial;
        }
        private static ConcurrentDictionary<string, GoodTypeEnum> GoodTypeLookup { get; }

        /// <summary>
        /// the cost
        /// </summary>
        public decimal Cost { get; }

        /// <summary>
        /// the color
        /// </summary>
        public List<int> Color { get; } = new List<int>();

        /// <summary>
        /// whether it is available from the start
        /// </summary>
        public bool AvailableFromStart { get; } = true;

        /// <summary>
        /// whether it is tradeable
        /// </summary>
        public bool Tradeable { get; } = true;

        /// <summary>
        /// whether it is money
        /// </summary>
        public bool Money { get; }

        /// <summary>
        /// if it is applicable for overseas penalty
        /// </summary>
        public bool OverseasPenalty { get; }
        /// <summary>
        /// the type of good
        /// </summary>
        public GoodTypeEnum GoodType { get; }

        /// <summary>
        /// private constructor to force use of factory methods
        /// </summary>
        /// <param name="node">the good node</param>
        /// <param name="goodType">the good type</param>
        private Good(KeyValueNode node, GoodTypeEnum goodType) : base(node.Key)
        {
            GoodType = goodType;
            Cost = node["cost"].Value.AsDecimal();
            var children = node.Children;


            if (children.ContainsKey("available_from_start"))
            {
                AvailableFromStart = node["available_from_start"].Value.AsBool();
            }

            if (children.ContainsKey("overseas_penalty"))
            {
                OverseasPenalty = node["overseas_penalty"].Value.AsBool();
            }

            if (children.ContainsKey("tradeable"))
            {
                Tradeable = node["tradeable"].Value.AsBool();
            }

            if (children.ContainsKey("money"))
            {
                Money = node["money"].Value.AsBool();
            }

            var colors = node["color"];
            foreach (var child in colors)
            {
                Color.Add(child.Key.AsInt());
            }
        }

        /// <summary>
        /// builds each section of goods
        /// </summary>
        /// <param name="node">the node to build</param>
        /// <returns>the section</returns>
        private static List<Good> BuildGoodSection(KeyValueNode node)
        {
            var type = GoodTypeLookup[node.Key];
            var result = new List<Good>();
            foreach (var child in node)
            {
                result.Add(new Good(child, type));
            }

            return result;
        }

        /// <summary>
        /// builds the goods
        /// </summary>
        /// <param name="node">the node to build the goods from</param>
        /// <returns>a list of goods</returns>
        public static List<Good> BuildGoods(KeyValueNode node)
        {
            var result = new List<Good>();
            foreach (var child in node)
            {
                result = result.Concat(BuildGoodSection(child)).ToList();
            }

            return result;
        }
    }
}
