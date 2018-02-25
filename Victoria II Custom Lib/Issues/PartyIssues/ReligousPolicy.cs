using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria_II_Custom_Lib.Interfaces;

namespace Victoria_II_Custom_Lib.Issues.PartyIssues
{
    public class ReligousPolicy : ILocalizable
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
