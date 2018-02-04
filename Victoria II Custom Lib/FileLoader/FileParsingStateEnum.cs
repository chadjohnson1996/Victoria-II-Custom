using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib
{
    public enum FileParsingStateEnum
    {
        Initial,
        ParsingKey,
        ParseValue,
        ParsingLeafValue,
        ParsingEscapedLeafValue,
        ParsingNestedValue,
        Finished,
        Comment
    }
}
