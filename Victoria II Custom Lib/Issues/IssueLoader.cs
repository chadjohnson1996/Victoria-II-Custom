using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria_II_Custom_Lib.FileLoader;

namespace Victoria_II_Custom_Lib.Issues
{
    public class IssueLoader : GameFileLoader
    {
        public IssueLoader Default { get; } = new IssueLoader();
        /// <summary>
        /// defines issue loader
        /// </summary>
        private IssueLoader() : base("/common/issues.txt")
        {

        }
    }
}
