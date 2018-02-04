using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib
{
    public class FileParsingState
    {
        public KeyValueNode Current { get; set; }
        public FileParsingStateEnum State { get; set; }

        /// <summary>
        /// the children start index
        /// </summary>
        public int ChildrenStartIndex { get; set; }

        /// <summary>
        /// the count of the children index
        /// </summary>
        public int ChildrenIndexCount { get; set; }
        public FileParsingState()
        {
            Current = new KeyValueNode();
            State = FileParsingStateEnum.Initial;
        }
    }
}
