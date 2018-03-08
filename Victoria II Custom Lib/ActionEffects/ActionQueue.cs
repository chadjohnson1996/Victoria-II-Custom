using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib.ActionEffects
{
    public class ActionQueue
    {
        /// <summary>
        /// the action dictionary
        /// </summary>
        private Dictionary<int, List<Action>> Actions { get; set; } = new Dictionary<int, List<Action>>();
        /// <summary>
        /// enqueues an action to be invoked after daysUntil
        /// </summary>
        /// <param name="daysUntil">days until the action should be invoked</param>
        /// <param name="action">the action to invoke</param>
        public void Enqueue(int daysUntil, Action action)
        {
            if (daysUntil < 0)
            {
                return;
            }

            if (daysUntil == 0)
            {
                action();
                return;
            }

            if (!Actions.ContainsKey(daysUntil))
            {
                Actions[daysUntil] = new List<Action>();
            }
            Actions[daysUntil].Add(action);
        }

        public void Decrement()
        {
            if (Actions.ContainsKey(1))
            {
                var toInvoke = Actions[1];
                foreach (var action in toInvoke)
                {
                    action();
                }
            }

            var oldActions = Actions;
            Actions = new Dictionary<int, List<Action>>();
            foreach (var action in oldActions)
            {
                if (action.Key != 1)
                {
                    Actions[action.Key - 1] = action.Value;
                }
            }
        }
    }
}
