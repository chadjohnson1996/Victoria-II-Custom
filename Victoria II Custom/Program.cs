using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria_II_Custom_Lib;

namespace Victoria_II_Custom
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        static async Task MainAsync(string[] args)
        {
            var testPath =
                "C:\\Users\\cjohnson\\Documents\\Paradox Interactive\\Victoria II\\save games\\Spain1844_07_07.v2";
            var fileParser = new FileParser();
            var test = await fileParser.Parse(testPath);
            var temp = 5;
        }
    }
}
