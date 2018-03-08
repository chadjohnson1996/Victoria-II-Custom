using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib.General
{
    /// <summary>
    /// rbg color
    /// </summary>
    public class Color
    {
        public byte R { get; set; }

        public byte G { get; set; }

        public byte B { get; set; }

        public Color()
        {

        }

        public Color(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
}
