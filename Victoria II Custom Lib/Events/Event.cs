using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
