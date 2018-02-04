using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib
{
    public class FileParsingState
    {
        /// <summary>
        /// the key builder
        /// </summary>
        public StringBuilder KeyBuilder { get;}

        /// <summary>
        /// the value builder
        /// </summary>
        public StringBuilder ValueBuilder { get; }
        public FileParsingStateEnum State { get; set; }

        /// <summary>
        /// the children start index
        /// </summary>
        public int ChildrenStartIndex { get; set; }

        /// <summary>
        /// the count of the children index
        /// </summary>
        public int ChildrenIndexCount { get; set; }

        /// <summary>
        /// the bracket count
        /// </summary>
        public int BracketCount { get; set; }
        public FileParsingState()
        {
            State = FileParsingStateEnum.Initial;
            KeyBuilder = new StringBuilder();
            ValueBuilder = new StringBuilder();
        }

        public KeyValueNode BuildNode()
        {
            var toReturn = new KeyValueNode();
            toReturn.Key = KeyBuilder.ToString();
            if (ValueBuilder.Length > 0)
            {
                toReturn.Value = ValueBuilder.ToString();
            }
            else
            {
                toReturn.Children = new List<KeyValueNode>();
            }

            return toReturn;
        }
    }
}
