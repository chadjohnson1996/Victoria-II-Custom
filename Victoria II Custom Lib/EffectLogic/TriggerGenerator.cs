using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib.EffectLogic
{
    public class TriggerGenerator
    {
        /// <summary>
        /// the handler dictionary
        /// </summary>
        private ConcurrentDictionary<string, Func<Scope, KeyValueNode, bool>> Handlers { get; } = new ConcurrentDictionary<string, Func<Scope, KeyValueNode, bool>>();

        public TriggerGenerator()
        {
            Handlers["AND"] = AndHandler;
        }
        /// <summary>
        /// root method to eval condition
        /// </summary>
        /// <param name="scope">the scope</param>
        /// <param name="root">the root</param>
        /// <returns></returns>
        public bool EvalCondition(Scope scope, KeyValueNode root)
        {
            return Handlers[root.Key.ToUpperInvariant()](scope, root);
        }

        /// <summary>
        /// handles logical and
        /// </summary>
        /// <param name="scope">the scope</param>
        /// <param name="root">the key value node</param>
        /// <returns>anding the conditions together</returns>
        private bool AndHandler(Scope scope, KeyValueNode root)
        {
            var toReturn = true;
            foreach (var child in root)
            {
                toReturn &= EvalCondition(scope, child);
            }

            return toReturn;
        }

        private bool OrHandler(Scope scope, KeyValueNode root)
        {
            foreach (var child in root)
            {
                if (EvalCondition(scope, child))
                {
                    return true;
                }
            }

            return false;
        }

        private bool NotHandler(Scope scope, KeyValueNode root)
        {
            foreach (var child in root)
            {
                return !EvalCondition(scope, child);
            }

            throw new ApplicationException("A 'NOT' Expression must have a body");
        }
    }
}
