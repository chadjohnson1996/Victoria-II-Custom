using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Victoria_II_Custom_Lib.Configuration;

namespace Victoria_II_Custom_Lib.FileLoader
{
    public class GameFileLoader
    {
        /// <summary>
        /// the cached value
        /// </summary>
        public KeyValueNode Data { get; set; }

        /// <summary>
        /// the relative path
        /// </summary>
        private string RelativePath { get; }

        private SemaphoreSlim LoadSem { get; } = new SemaphoreSlim(1);

        /// <summary>
        /// whether it should use the name as the key
        /// </summary>
        public bool UseNameAsKey { get; }

        public Func<FileInfo, string> KeyProvider { get; }

        /// <summary>
        /// loads file at the relative path
        /// </summary>
        /// <param name="relativePath">the relative path to load</param>
        /// <param name="keyProvider">the key provider</param>
        public GameFileLoader(string relativePath,  Func<FileInfo, string> keyProvider = null)
        {
            RelativePath = relativePath;
            KeyProvider = keyProvider;
        }

        public async Task<KeyValueNode> Load()
        {
            if (Data != null)
            {
                return Data;
            }
            await LoadSem.WaitAsync();
            try
            {
                if (Data != null)
                {
                    return Data;
                }

                var path = Path.Combine(Config.Default.RootDirectory, RelativePath);
                var toReturn = await new FileParser().Parse(path);

                if (KeyProvider != null)
                {
                    var fileInfo = new FileInfo(path);
                    toReturn.Key = KeyProvider(fileInfo);
                }
                Data = toReturn;
                return toReturn;
            }
            finally
            {
                LoadSem.Release();
            }
        }
    }
}
