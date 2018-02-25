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
        public GameFileLoader IssueLoader { get; } = new GameFileLoader("/common/issues.txt");

        public GameFolderLoader DecisionsLoader { get; } = new GameFolderLoader("/decisions");
        public async Task Load()
        {
            
        }
    }
}
