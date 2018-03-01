using System;
using System.Collections.Generic;
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
        public GameFolderLoader PopLoader { get; } = new GameFolderLoader("poptypes", 0, x =>
        {
            var name = x.Name;
            var extension = x.Extension;
            return name.Substring(0, name.Length - extension.Length - 1);
        });

        /// <summary>
        /// the unit loader
        /// </summary>
        public GameFolderLoader UnitLoader { get; } = new GameFolderLoader("units");

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
        public async Task Load()
        {
            await Task.WhenAll(IssueLoader.Load(), 
                DecisionsLoader.Load(),
                EventsLoader.Load(),
                InventionsLoader.Load(),
                TechnologiesLoader.Load(),
                PopLoader.Load(),
                UnitLoader.Load(),
                ProvinceLoader.Load()
                );
        }
    }
}
