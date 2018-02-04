﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib
{
    public class KeyValueNode
    {
        /// <summary>
        /// the key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// the children
        /// </summary>
        public List<KeyValueNode> Children { get; set; }

        /// <summary>
        /// the value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// whether or not the node is a leaf
        /// </summary>
        public bool IsLeaf => Value != null;
    }
}
