using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria_II_Custom_Lib.General;
using Victoria_II_Custom_Lib.Modifiers;

namespace Victoria_II_Custom_Lib.Countries
{
    public class Country : LocalizableObject, IScope
    {
        public string FromCountry { get; set; }
        public string ThisCountry { get; set; }
        public IScope Parent { get; set; }
        public IScope Previous { get; set; }
        public ScopeTypeEnum Type { get; set; }
        public GameState State { get; set; }

        private Country(GameState state, KeyValueNode root) : base(root.Key)
        {
            
        }


        public void ApplyModifier(Modifier modifier)
        {
            throw new NotImplementedException();
        }
    }
}
