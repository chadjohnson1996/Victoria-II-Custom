using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria_II_Custom_Lib;
using Victoria_II_Custom_Lib.LocalizationInfo;

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
            var test5 = DateTime.Parse("1861.1.2");
            await Bootstrap.Init();
            var testTwo = Localization.Data;
            var testPath =
                "C:\\Users\\cjohnson\\Documents\\Paradox Interactive\\Victoria II\\save games\\Spain1844_07_07.v2";
            //testPath = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Victoria 2\\common\\pop_types.txt";
            var fileParser = new FileParser();
            var test = await fileParser.Parse(testPath);
            var temp = 5;
        }
    }
}
