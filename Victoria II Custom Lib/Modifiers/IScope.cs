using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib.Modifiers
{
    public interface IScope
    {
        /// <summary>
        /// the from country
        /// </summary>
        string FromCountry { get; set; }

        /// <summary>
        /// the country
        /// </summary>
        string ThisCountry { get; set; }

        /// <summary>
        /// the parent scope
        /// </summary>
        IScope Parent { get; set; }

        /// <summary>
        /// the previous scope
        /// </summary>
        IScope Previous { get; set; }

        /// <summary>
        /// the scope type enum
        /// </summary>
        ScopeTypeEnum Type { get; set; }

        /// <summary>
        /// the game state
        /// </summary>
        GameState State { get; set; }
        /// <summary>
        /// applies the given modifier to the scope
        /// </summary>
        /// <param name="modifier">the modifier to apply</param>
        void ApplyModifier(Modifier modifier);
    }
}
