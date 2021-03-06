﻿using System;
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

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object o)
        {
            if (!(o is LocalizableObject oLocalizable))
            {
                return false;
            }

            return oLocalizable.Name == Name;
        }
    }
}
