using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Victoria_II_Custom_Lib.Extensions;
using Victoria_II_Custom_Lib.Goods;

namespace Victoria_II_Custom_Lib.Units
{
    public abstract class Unit
    {
        /// <summary>
        /// the unit type map
        /// </summary>
        private static ConcurrentDictionary<string, UnitTypeEnum> UnitTypeMap { get; }

        static Unit()
        {
            UnitTypeMap = new ConcurrentDictionary<string, UnitTypeEnum>();
            UnitTypeMap["big_ship"] = UnitTypeEnum.BigShip;
            UnitTypeMap["cavalry"] = UnitTypeEnum.Cavalry;
            UnitTypeMap["land"] = UnitTypeEnum.Land;
            UnitTypeMap["light_ship"] = UnitTypeEnum.LightShip;
            UnitTypeMap["transport"] = UnitTypeEnum.Transport;
            UnitTypeMap["special"] = UnitTypeEnum.Special;
            UnitTypeMap["support"] = UnitTypeEnum.Support;
        }

        /// <summary>
        /// the unit name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// the unit type
        /// </summary>
        public UnitTypeEnum UnitType { get; }

        /// <summary>
        /// the unit icon
        /// </summary>
        public int Icon { get; }

        /// <summary>
        /// the sprite
        /// </summary>
        public string Sprite { get; }

        /// <summary>
        /// whether it is active by default
        /// </summary>
        public bool Active { get; }

        /// <summary>
        /// the move sound
        /// </summary>
        public string MoveSound { get; }

        /// <summary>
        /// the select sound
        /// </summary>
        public string SelectSound { get; }

        /// <summary>
        /// whether it has a floating flag
        /// </summary>
        public bool FloatingFlag { get; }

        /// <summary>
        /// its priority
        /// </summary>
        public int Priority { get; }

        /// <summary>
        /// the max strength of the unit
        /// </summary>
        public int MaxStrength { get; }

        /// <summary>
        /// the default organization
        /// </summary>
        public int DefaultOrganization { get; }

        /// <summary>
        /// the maximum speed
        /// </summary>
        public int MaximumSpeed { get; }

        /// <summary>
        /// the weighted value
        /// </summary>
        public decimal WeightedValue { get; }

        /// <summary>
        /// the build time
        /// </summary>
        public int BuildTime { get; }

        /// <summary>
        /// the build cost
        /// </summary>
        public Dictionary<Good, decimal> BuildCost { get; } = new Dictionary<Good, decimal>();

        /// <summary>
        /// the supply consumption
        /// </summary>
        public decimal SupplyConsumption { get; }

        /// <summary>
        /// the supply cost
        /// </summary>
        public Dictionary<Good, decimal> SupplyCost { get; } = new Dictionary<Good, decimal>();

        /// <summary>
        /// creates a unit
        /// </summary>
        /// <param name="root">the root</param>
        protected Unit(KeyValueNode root)
        {
            Name = root.Key;
            Sprite = root["sprite"].Value;
            UnitType = UnitTypeMap[root["unit_type"].Value];
            Active = root["active"].Value.AsBool();
            Icon = root["icon"].Value.AsInt();
            MoveSound = root["move_sound"]?.Value;
            SelectSound = root["select_sound"]?.Value;
            FloatingFlag = root["floating_flag"].Value.AsBool();
            Priority = root["priority"].Value.AsInt();
            MaxStrength = root["max_strength"].Value.AsInt();
            DefaultOrganization = root["default_orgranization"].Value.AsInt();
            MaximumSpeed = root["maximum_speed"].Value.AsInt();
            WeightedValue = root["weighted_value"].Value.AsDecimal();
            BuildTime = root["build_time"].Value.AsInt();
            SupplyConsumption = root["supply_consumption"].Value.AsDecimal();
            BuildCost = Good.PopulateCost(root["build_cost"]);
            SupplyCost = Good.PopulateCost(root["supply_cost"]);
        }

        public static Unit Factory(KeyValueNode root)
        {
            var type = root["type"].Value;
            switch (type)
            {
                case "naval":
                    return new NavalUnit(root);
                case "land":
                    return new LandUnit(root);
                default:
                    throw new ApplicationException("Invalid type for unit");
            }
        }
    }
}
