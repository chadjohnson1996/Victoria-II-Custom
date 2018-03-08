using System;
using System.Collections.Concurrent;
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

        /// <summary>
        /// creates a country
        /// </summary>
        /// <param name="state">the state</param>
        /// <param name="root">the root</param>
        private Country(GameState state, KeyValueNode root) : base(root.Key)
        {
            
        }

        public static Country Factory(GameState state, KeyValueNode root)
        {

            var country = new Country(state, root);
            var governmentLocation = root.Value;
            var countryFileNode = state.Loaders.CountryDirectoryLoader.Data.First(x => x.Key == governmentLocation);

            return country;
        }

        public static ConcurrentDictionary<string, Country> GetAllCountries(GameState state)
        {
            var result = new ConcurrentDictionary<string, Country>();
            var rootNode = state.Loaders.CountryLoader.Data;
            foreach (var node in rootNode)
            {
                if (node.Key == "dynamic_tags")
                {
                    //TODO add handling for dynamic tags
                    continue;
                }
                result[node.Key] = Factory(state, node);
            }

            return result;
        }


        public void ApplyModifier(Modifier modifier)
        {
            throw new NotImplementedException();
        }
    }
}
