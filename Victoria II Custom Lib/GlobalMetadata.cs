using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria_II_Custom_Lib.ActionEffects;

namespace Victoria_II_Custom_Lib
{
    public class GlobalMetadata
    {
        /// <summary>
        /// the global flags
        /// </summary>
        public HashSet<string> GlobalFlags { get; } = new HashSet<string>();

        /// <summary>
        /// flags for fire only once events that have already fired
        /// </summary>
        public HashSet<int> FireOnlyOnce { get; } = new HashSet<int>();

        /// <summary>
        /// the variables
        /// </summary>
        public Dictionary<string, decimal> Variables { get; } = new Dictionary<string, decimal>();

        /// <summary>
        /// dictionary mapping month names to their numeric representation
        /// </summary>
        public Dictionary<string, int> MonthMap { get; } = new Dictionary<string, int>();

        /// <summary>
        /// the action queue
        /// </summary>
        public ActionQueue ActionQueue { get; set; } = new ActionQueue();

        public DateTime Date { get; set; } = DateTime.MinValue;

        /// <summary>
        /// the enabled canals
        /// </summary>
        public HashSet<int> EnabledCanals { get; } = new HashSet<int>();

        /// <summary>
        /// the infamy limit
        /// </summary>
        public decimal InfamyLimit { get; set; }

        public GlobalMetadata()
        {
            MonthMap["JANUARY"] = 1;
            MonthMap["FEBRUARY"] = 2;
            MonthMap["MARCH"] = 3;
            MonthMap["APRIL"] = 4;
            MonthMap["MAY"] = 5;
            MonthMap["JUNE"] = 6;
            MonthMap["JULY"] = 7;
            MonthMap["AUGUST"] = 8;
            MonthMap["SEPTEMBER"] = 9;
            MonthMap["OCTOBER"] = 10;
            MonthMap["NOVEMBER"] = 11;
            MonthMap["DECEMBER"] = 12;
        }

        /// <summary>
        /// sets a flag from the global flags
        /// </summary>
        /// <param name="flag">the flags to set</param>
        public void SetFlag(string flag)
        {
            GlobalFlags.Add(flag);
        }

        /// <summary>
        /// removes a flag from the global flag
        /// </summary>
        /// <param name="flag">the flag</param>
        public void RemoveFlag(string flag)
        {
            GlobalFlags.Remove(flag);
        }

        /// <summary>
        /// sets a variable
        /// </summary>
        /// <param name="name">the variable to set</param>
        /// <param name="value">the value</param>
        public void SetVariable(string name, decimal value)
        {
            Variables[name] = value;
        }

        /// <summary>
        /// adds the given value to the variable
        /// </summary>
        /// <param name="name">the variable</param>
        /// <param name="toAdd">the value to add</param>
        public void ChangeVariable(string name, decimal toAdd)
        {
            Variables[name] += toAdd;
        }
    }
}
