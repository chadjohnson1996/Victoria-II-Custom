using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria_II_Custom_Lib.General;
using Victoria_II_Custom_Lib.Goods;

namespace Victoria_II_Custom_Lib.Buildings
{
    public class Building : LocalizableObject
    {
        public int Cost { get; }

        public ConcurrentDictionary<Good, int> GoodCost { get; } = new ConcurrentDictionary<Good, int>();

        public BuildingType BuildingType { get; }

        public int Time { get; }

        public bool Visability { get; }

        public bool OnMap { get; }

        public int MaxLevel { get; }

        public bool Province { get; }

        public bool Factory { get; }

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

        private Building(KeyValueNode root) : base(root.Key)
        {

        }

        public static List<Building> Bootstrap(KeyValueNode root)
        {
            return root.Select(x => new Building(x)).ToList();
        }
    }
}
