using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria_II_Custom_Lib.LocalizationInfo;

namespace Victoria_II_Custom_Lib
{
    public class Bootstrap
    {
        public static async Task Init()
        {
            await Localization.Init();
        }
    }
}
