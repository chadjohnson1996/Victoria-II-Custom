using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Victoria_II_Custom_Lib.Configuration;

namespace Victoria_II_Custom_Lib.LocalizationInfo
{
    public class Localization
    {
        /// <summary>
        /// the localization data
        /// </summary>
        public static ConcurrentDictionary<string, IList<string>> Data { get; } = new ConcurrentDictionary<string, IList<string>>();

        public static async Task Init()
        {
            var subDir = new DirectoryInfo(Path.Combine(Config.Default.RootDirectory, "localisation"));

            foreach (var file in subDir.GetFiles())
            {
                using (var sr = new StreamReader(File.OpenRead(file.FullName)))
                {
                    var config = new CsvHelper.Configuration.Configuration();
                    config.BadDataFound = x => Console.WriteLine(x.ToString());
                    config.Delimiter = ";";
                    var parser = new CsvParser(sr, config);
                   
                    while (true)
                    {
                        var row = await parser.ReadAsync();
                        if (row == null)
                        {
                            break;
                        }
                        Data[row[0]] = new ArraySegment<string>(row, 1, row.Length - 1);
                    }
                }
                
            }
        }
    }
}
