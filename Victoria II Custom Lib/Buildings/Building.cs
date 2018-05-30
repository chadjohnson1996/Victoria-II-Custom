using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria_II_Custom_Lib.Extensions;
using Victoria_II_Custom_Lib.General;
using Victoria_II_Custom_Lib.Goods;

namespace Victoria_II_Custom_Lib.Buildings
{
    public class Building : LocalizableObject
    {

        static Building()
        {
            BuildingTypeMap["factory"] = BuildingType.Factory;
            BuildingTypeMap["naval_base"] = BuildingType.NavalBase;
            BuildingTypeMap["infrastructure"] = BuildingType.Infrastructure;
            BuildingTypeMap["fort"] = BuildingType.Fort;
        }

        private static ConcurrentDictionary<string, BuildingType> BuildingTypeMap { get; } = new ConcurrentDictionary<string, BuildingType>();

        public int Cost { get; }

        public ConcurrentDictionary<Good, int> GoodCost { get; } = new ConcurrentDictionary<Good, int>();

        public BuildingType BuildingType { get; }

        public int Time { get; }

        public bool Visability { get; }

        public bool OnMap { get; }

        public int MaxLevel { get; }

        public bool Province { get; }

        public decimal Infrastructure { get; }

        public decimal MovementCost { get; }

        public bool PopBuildFactory { get; }

        public bool SpawnRailwayTrack { get; }

        public int NavalCapacity { get; }

        public bool Capital { get; }

        public List<int> ColonialPoints { get; }

        public int ColonialRange { get; }

        public bool OnePerState { get; }

        public decimal LocalShipBuildTime { get; }

        public int FortLevel { get; }

        public decimal CompletionSize { get; }


        public bool AdvancedFactory { get; }

        public bool DefaultEnabled { get; }
        /// <summary>
        /// probably sound effect
        /// </summary>
        public string OnCompletion { get; }

        public string ProductionType { get; }

        private Building(GameState state, KeyValueNode root) : base(root.Key)
        {
            BuildingType = BuildingTypeMap[root["type"].Value];
            Cost = root["cost"]?.Value?.AsInt() ?? 0;
            Time = root["time"].Value.AsInt();
            Visability = root["visability"]?.Value?.AsBool() ?? false;
            OnMap = root["onmap"]?.Value?.AsBool() ?? false;
            MaxLevel = root["MaxLevel"]?.Value?.AsInt() ?? Int32.MaxValue;
            Province = root["province"]?.Value?.AsBool() ?? false;
            Infrastructure = root["infrastructure"]?.Value?.AsDecimal() ?? 0;
            MovementCost = root["movement_cost"]?.Value?.AsDecimal() ?? 0;
            PopBuildFactory = root["pop_build_factory"]?.Value?.AsBool() ?? false;
            SpawnRailwayTrack = root["spawn_railway_track"]?.Value?.AsBool() ?? false;
            NavalCapacity = root["naval_capacity"]?.Value?.AsInt() ?? 0;
            Capital = root["capital"]?.Value?.AsBool() ?? false;
            ColonialPoints = root["colonial_points"]?.Select(x => x.Key.AsInt())?.ToList();
            ColonialRange = root["colonial_range"]?.Value?.AsInt() ?? 0;
            OnePerState = root["one_per_state"]?.Value?.AsBool() ?? false;
            LocalShipBuildTime = root["local_ship_build"]?.Value?.AsDecimal() ?? 0;
            FortLevel = root["fort_level"]?.Value?.AsInt() ?? 0;
            CompletionSize = root["completion_size"]?.Value?.AsDecimal() ?? 0;
            AdvancedFactory = root["advanced_factory"]?.Value?.AsBool() ?? false;
            DefaultEnabled = root["default_enabled"]?.Value?.AsBool() ?? false;
            ProductionType = root["production_type"]?.Value;
            OnCompletion = root["on_completion"]?.Value;

            SetGoods(state, root);
        }

        private void SetGoods(GameState state, KeyValueNode root)
        {
            var goodsMap = state.GoodState.Goods;
            var goods = root["goods_cost"];
            foreach (var good in goods)
            {
                GoodCost[goodsMap[good.Key]] = good.Value.AsInt();
            }
        }
        public static ConcurrentDictionary<string, Building> Bootstrap(GameState state, KeyValueNode root)
        {
            var result = root.Select(x => new Building(state, x)).ToList();

            var toReturn = new ConcurrentDictionary<string, Building>();

            foreach (var building in result)
            {
                toReturn[building.Name] = building;
            }

            return toReturn;
        }
    }
}
