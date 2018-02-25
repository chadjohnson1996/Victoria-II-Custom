using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria_II_Custom_Lib.FileLoader.Loaders;
using Victoria_II_Custom_Lib.General;

namespace Victoria_II_Custom_Lib.Issues.PartyIssues
{
    public class WarPolicy : LocalizableObject
    {

        //public static WarPolicy Default { get; } = new WarPolicy();
        private WarPolicy(string name) : base(name)
        {

        }

        public async Task Init()
        {
            var node = await IssueLoader.Default.Load();
            //foreach(var entry in node[])
        }
    }
}
