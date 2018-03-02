using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib.Modifiers
{
    public interface IScope
    {
        /// <summary>
        /// the from country
        /// </summary>
        string FromCountry { get; set; }

        /// <summary>
        /// the country
        /// </summary>
        string Country { get; set; }
    }
}
