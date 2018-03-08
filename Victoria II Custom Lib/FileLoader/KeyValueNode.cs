using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria_II_Custom_Lib.Extensions;
using Victoria_II_Custom_Lib.General;

namespace Victoria_II_Custom_Lib
{
    public class KeyValueNode : IEnumerable<KeyValueNode>
    {
        /// <summary>
        /// the key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// the children
        /// </summary>
        public Dictionary<string, List<KeyValueNode>> Children { get; set; }

        /// <summary>
        /// the value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// whether or not the node is a leaf
        /// </summary>
        public bool IsLeaf => Value != null;

        public KeyValueNode this[string i]
        {
            get
            {
                if (!Children.ContainsKey(i))
                {
                    return null;
                }
                return Children[i].FirstOrDefault();
            }
            set
            {
                if (!Children.ContainsKey(i))
                {
                    Children[i] = new List<KeyValueNode>();
                }
                Children[i].Add(value);
            }
        }

        public IEnumerator<KeyValueNode> GetEnumerator()
        {
            foreach (var value in Children.Values)
            {
                foreach (var child in value)
                {
                    yield return child;
                }
            }
        }

        public override string ToString()
        {
            return Key;
        }

        /// <summary>
        /// converts a key value node to a color
        /// </summary>
        /// <returns>the color</returns>
        public Color ToColor()
        {
            var colors = new List<int>();

            foreach (var child in this)
            {
                colors.Add(child.Value.AsInt());
            }
            return new Color((byte)colors[0], (byte)colors[1], (byte)colors[2]);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
