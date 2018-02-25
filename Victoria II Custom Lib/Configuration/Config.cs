using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib.Configuration
{
    public class Config
    {
        /// <summary>
        /// the default configuration
        /// </summary>
        public static Config Default { get; } = new Config();

        public string RootDirectory { get; } = "C:\\Users\\cjohnson\\Downloads\\Victoria 2 zip";

    }
}
