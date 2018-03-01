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
        /// the key provider
        /// </summary>
        public Func<FileInfo, string> KeyProvider { get; }

        /// <summary>
        /// whether it should recurse
        /// </summary>
        public bool Recurse { get; }

        /// <summary>
        /// the game folder loader
        /// </summary>
        /// <param name="folderName">the folder name</param>
        /// <param name="rootDepth">the root depth that we care about</param>
        /// <param name="keyProvider">the key provider</param>
        /// <param name="recurse">whether or not is should recurse through all the folders</param>
        public GameFolderLoader(string folderName, int rootDepth = 1, Func<FileInfo, string> keyProvider = null, bool recurse = false) 
        {
            RootDepth = rootDepth;
            FolderName = folderName;
            KeyProvider = keyProvider;
            Recurse = recurse;
        }

        private List<DirectoryInfo> GetDirectories()
        {
            var result = new List<DirectoryInfo>();
            var toProcess = new Queue<DirectoryInfo>();
            var path = Path.Combine(GlobalConfig.RootDirectory, FolderName);
            var root = new DirectoryInfo(path);


            result.Add(root);

            if (!Recurse)
            {
                return result;
            }
            toProcess.Enqueue(root);
            while (toProcess.Count > 0)
            {
                var current = toProcess.Dequeue();
                foreach (var dir in current.EnumerateDirectories())
                {
                    result.Add(dir);
                    toProcess.Enqueue(dir);
                }
            }


            return result;
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

                var directories = GetDirectories();
                var result = new List<KeyValueNode>();

                foreach (var dir in directories)
                {
                    foreach (var file in dir.GetFiles())
                    {
                        var loader = new GameFileLoader(file.FullName, KeyProvider);
                        var toAdd = await loader.Load();

                        var concernedWith = new List<KeyValueNode> { toAdd };

                        var recurseTo = RootDepth;

                        while (recurseTo > 0)
                        {
                            concernedWith = concernedWith.SelectMany(x => x.ToList()).ToList();
                            recurseTo--;
                        }

                        result = result.Concat(concernedWith).ToList();
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
