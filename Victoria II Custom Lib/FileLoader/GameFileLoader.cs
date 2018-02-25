using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib.FileLoader
{
    public class GameFileLoader
    {
        /// <summary>
        /// the cached value
        /// </summary>
        private KeyValueNode Cache { get; set; }

        /// <summary>
        /// the relative path
        /// </summary>
        private string RelativePath { get; }

        private SemaphoreSlim LoadSem { get; } = new SemaphoreSlim(1);

        /// <summary>
        /// whether it should use the name as the key
        /// </summary>
        public bool UseNameAsKey { get; }

        /// <summary>
        /// loads file at the relative path
        /// </summary>
        /// <param name="relativePath">the relative path to load</param>
        /// <param name="useNameAsKey">whether it should use the name as the key</param>
        public GameFileLoader(string relativePath,  bool useNameAsKey = false)
        {
            RelativePath = relativePath;
            UseNameAsKey = useNameAsKey;
        }

        public async Task<KeyValueNode> Load()
        {
            if (Cache != null)
            {
                return Cache;
            }
            await LoadSem.WaitAsync();
            try
            {
                if (Cache != null)
                {
                    return Cache;
                }

                var path = Path.Combine(GlobalConfig.RootDirectory, RelativePath);
                var toReturn = await new FileParser().Parse(path);

                if (UseNameAsKey)
                {
                    var fileInfo = new FileInfo(path);
                    var name = fileInfo.Name;
                    toReturn.Key = name.Substring(0, name.Length - fileInfo.Extension.Length - 1);
                }
                Cache = toReturn;
                return toReturn;
            }
            finally
            {
                LoadSem.Release();
            }
        }
    }
}
