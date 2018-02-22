using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib.General
{
    public class LocalizableObject
    {
        /// <summary>
        /// the name
        /// </summary>
        public string Name { get; protected set; }
        
        /// <summary>
        /// the display name
        /// </summary>
        public string DisplayName { get; protected set; }
        public LocalizableObject(string name)
        {
            Name = name;
        }
    }
}
