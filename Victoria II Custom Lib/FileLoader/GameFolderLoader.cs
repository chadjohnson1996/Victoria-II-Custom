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
        private List<KeyValueNode> Cache { get; set; }
        
        /// <summary>
        /// the folder lock
        /// </summary>
        private SemaphoreSlim Lock { get; } = new SemaphoreSlim(1);
        /// <summary>
        /// the folder name
        /// </summary>
        public string FolderName { get; }
        
        /// <summary>
        /// the root depth that we care about in file
        /// </summary>
        public int RootDepth { get; }

        /// <summary>
        /// the game folder loader
        /// </summary>
        /// <param name="folderName">the folder name</param>
        /// <param name="rootDepth">the root depth that we care about</param>
        public GameFolderLoader(string folderName, int rootDepth = 0)
        {
            RootDepth = rootDepth;
            FolderName = folderName;
        }

        public async Task<List<KeyValueNode>> Load()
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

                var path = Path.Combine(GlobalConfig.RootDirectory, FolderName);
                var dir = new DirectoryInfo(path);
                var result = new List<KeyValueNode>();
                var count = 0;
                foreach (var file in dir.GetFiles())
                {
                    var loader = new GameFileLoader(file.FullName);
                    var toAdd = await loader.Load();

                    var concernedWith = toAdd.ToList();
                    count += concernedWith.Count;
                    var recurseTo = RootDepth;
                    if (file.FullName.Contains("event"))
                    {
                        var test = 5;
                    }
                    while (recurseTo > 0)
                    {
                        concernedWith = concernedWith.SelectMany(x => x.ToList()).ToList();
                        recurseTo--;
                    }
                    
                    result = result.Concat(concernedWith).ToList();
                    //Console.WriteLine(result.Count);
                }

                Console.WriteLine(count);
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
