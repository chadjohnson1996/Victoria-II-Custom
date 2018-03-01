using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib.FileLoader.Loaders
{
    public class GameStateLoaders
    {
        /// <summary>
        /// the issue loader
        /// </summary>
        public GameFileLoader IssueLoader { get; } = new GameFileLoader("common/issues.txt");

        /// <summary>
        /// the decisions loader
        /// </summary>
        public GameFolderLoader DecisionsLoader { get; } = new GameFolderLoader("decisions", 2);

        /// <summary>
        /// the events loader
        /// </summary>
        public GameFolderLoader EventsLoader { get; } = new GameFolderLoader("events");

        /// <summary>
        /// the inventions loader
        /// </summary>
        public GameFolderLoader InventionsLoader { get; } = new GameFolderLoader("inventions");

        /// <summary>
        /// added the technologies folder
        /// </summary>
        public GameFolderLoader TechnologiesLoader { get; } = new GameFolderLoader("technologies");

        /// <summary>
        /// the pop loader
        /// </summary>
        public GameFolderLoader PopTypeLoader { get; } = new GameFolderLoader("poptypes", 0, x =>
        {
            var name = x.Name;
            var extension = x.Extension;
            return name.Substring(0, name.Length - extension.Length - 1);
        });

        /// <summary>
        /// the unit loader
        /// </summary>
        public GameFolderLoader UnitLoader { get; } = new GameFolderLoader("units");

        public GameFolderLoader DiplomacyLoader { get; } = new GameFolderLoader("history/diplomacy");

        /// <summary>
        /// the game folder loader
        /// </summary>
        public GameFolderLoader ProvinceLoader { get; } = new GameFolderLoader("history/provinces", 0, x =>
            {
                var name = x.Name;
                var index = name.IndexOf(" ");
                var key = name.Substring(0, index + 1).Trim();
                return key;
            }, 
            true);

        /// <summary>
        /// the pop loader
        /// </summary>
        public GameFolderLoader PopLoader { get; } =
            new GameFolderLoader("history/pops", 0, x => x.Directory.Name, true);

        /// <summary>
        /// loads the oob
        /// </summary>
        public GameFolderLoader OobLoader { get; } = new GameFolderLoader("history/units", 0, x =>
        {
            var name = x.DirectoryName;
            var indexFrom = "units";
            var lastindex = name.LastIndexOf(indexFrom) + indexFrom.Length;
            var dirKey = name.Substring(lastindex);
            var fileName = x.Name;
            var key = Path.Combine(dirKey, fileName);
            return key;
        }, true);

        /// <summary>
        /// loads the bookmarks
        /// </summary>
        public GameFileLoader BookmarkLoader { get; } = new GameFileLoader("common/bookmarks.txt");

        /// <summary>
        /// the building loader
        /// </summary>
        public GameFileLoader BuildingLoader { get; } = new GameFileLoader("common/buildings.txt");

        /// <summary>
        /// the cb loader
        /// </summary>
        public GameFileLoader CbLoader { get; } = new GameFileLoader("common/cb_types.txt");

        /// <summary>
        /// the color loader
        /// </summary>
        public GameFileLoader CotColorLoader { get; } = new GameFileLoader("common/cot_colors.txt");

        public async Task Load()
        {
            await Task.WhenAll(IssueLoader.Load(), 
                DecisionsLoader.Load(),
                EventsLoader.Load(),
                InventionsLoader.Load(),
                TechnologiesLoader.Load(),
                PopLoader.Load(),
                UnitLoader.Load(),
                ProvinceLoader.Load(),
                PopTypeLoader.Load(),
                DiplomacyLoader.Load(),
                OobLoader.Load(),
                BookmarkLoader.Load(),
                BuildingLoader.Load(),
                CbLoader.Load(),
                CotColorLoader.Load()
                );
        }
    }
}
