using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria_II_Custom_Lib.Modifiers;

namespace Victoria_II_Custom_Lib.Events
{
    public class Event
    {
        /// <summary>
        /// the event id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// the event title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// whether the event is triggered only
        /// </summary>
        public bool IsTriggeredOnly { get; set; }

        /// <summary>
        /// whether it should be fired only once
        /// </summary>
        public bool FireOnlyOnce { get; set; }

        /// <summary>
        /// whether it should be fired
        /// </summary>
        public bool Fired { get; set; }

        /// <summary>
        /// fires an event with the target from the source
        /// </summary>
        /// <param name="target">the target</param>
        /// <param name="from">the source</param>
        public void Fire(IScope target, IScope from)
        {

        }

        public bool TriggerValid(IScope scope)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// gets the mean time to happen for the scope
        /// </summary>
        /// <param name="scope">the scope</param>
        /// <returns>the mean time to happen</returns>
        public decimal MeanTimeToHappen(IScope scope)
        {
            throw new NotImplementedException();
        }
    }
}
