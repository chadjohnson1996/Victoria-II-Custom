using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib.FileLoader
{
    public class GameFolderLoader
    {
        /// <summary>
        /// the cache
        /// </summary>
        private KeyValueNode Cache { get; set; }
        
        /// <summary>
        /// the folder lock
        /// </summary>
        private SemaphoreSlim Lock { get; } = new SemaphoreSlim(1);
        /// <summary>
        /// the folder name
        /// </summary>
        public string FolderName { get; }
        /// <summary>
        /// the game folder loader
        /// </summary>
        /// <param name="folderName">the folder name</param>
        public GameFolderLoader(string folderName)
        {
            FolderName = folderName;
        }

        public async Task<KeyValueNode> Load()
        {
            if (Cache != null)
            {
                return Cache;
            }

            await Lock.WaitAsync();
            try
            {
                if (Cache != null)
                {
                    return Cache;
                }

                var dir = new DirectoryInfo(FolderName);
                var result = new KeyValueNode();
                result.Key = "root";
                result.Children = new Dictionary<string, KeyValueNode>();
                foreach (var file in dir.GetFiles())
                {
                    var loader = new GameFileLoader(file.FullName);
                    var toAdd = await loader.Load();
                    foreach (var value in toAdd.Children)
                    {
                        result[value.Key] = value.Value;
                    }
                }
                Cache = result;
                return Cache;
            }
            finally
            {
                Lock.Release();
            }
        }
    }
}
