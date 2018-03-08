using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria_II_Custom_Lib.Modifiers;

namespace Victoria_II_Custom_Lib.ActionEffects
{
    public class ActionEffectFactory
    {
        /// <summary>
        /// the action effect factory
        /// </summary>
        /// <param name="effects">the effects</param>
        /// <returns>a function to invoke the action on the scope</returns>
        public Action<IScope> Factory(IList<KeyValueNode> effects)
        {

            throw new NotImplementedException();
        }
    }
}
