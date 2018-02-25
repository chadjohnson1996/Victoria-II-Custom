﻿using System;
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
        public GameFolderLoader DecisionsLoader { get; } = new GameFolderLoader("decisions", 1);

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
        public async Task Load()
        {
            await Task.WhenAll(IssueLoader.Load(), 
                DecisionsLoader.Load(),
                EventsLoader.Load(),
                InventionsLoader.Load(),
                TechnologiesLoader.Load()
                );
        }
    }
}
