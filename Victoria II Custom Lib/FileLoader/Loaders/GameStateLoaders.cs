using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib.FileLoader.Loaders
{
    public class GameStateLoaders
    {
        /// <summary>
        /// the decisions loader
        /// </summary>
        public GameFolderLoader DecisionsLoader { get; } = new GameFolderLoader("decisions", 2);

        /// <summary>
        /// the events loader
        /// </summary>
        public GameFolderLoader EventsLoader { get; } = new GameFolderLoader("events");

        /// <summary>
        /// the inventions loader
        /// </summary>
        public GameFolderLoader InventionsLoader { get; } = new GameFolderLoader("inventions");

        /// <summary>
        /// added the technologies folder
        /// </summary>
        public GameFolderLoader TechnologiesLoader { get; } = new GameFolderLoader("technologies");

        /// <summary>
        /// the pop loader
        /// </summary>
        public GameFolderLoader PopTypeLoader { get; } = new GameFolderLoader("poptypes", 0, x =>
        {
            var name = x.Name;
            var extension = x.Extension;
            return name.Substring(0, name.Length - extension.Length - 1);
        });

        /// <summary>
        /// the unit loader
        /// </summary>
        public GameFolderLoader UnitLoader { get; } = new GameFolderLoader("units");

        #region History
        /// <summary>
        /// the diplomacy loader
        /// </summary>
        public GameFolderLoader DiplomacyLoader { get; } = new GameFolderLoader("history/diplomacy");

        /// <summary>
        /// the game folder loader
        /// </summary>
        public GameFolderLoader ProvinceLoader { get; } = new GameFolderLoader("history/provinces", 0, x =>
            {
                var name = x.Name;
                var index = name.IndexOf(" ");
                var key = name.Substring(0, index + 1).Trim();
                return key;
            },
            true);

        /// <summary>
        /// the pop loader
        /// </summary>
        public GameFolderLoader PopLoader { get; } =
            new GameFolderLoader("history/pops", 0, x => x.Directory.Name, true);

        /// <summary>
        /// loads the oob
        /// </summary>
        public GameFolderLoader OobLoader { get; } = new GameFolderLoader("history/units", 0, x =>
        {
            var name = x.DirectoryName;
            var indexFrom = "units";
            var lastindex = name.LastIndexOf(indexFrom) + indexFrom.Length;
            var dirKey = name.Substring(lastindex);
            var fileName = x.Name;
            var key = Path.Combine(dirKey, fileName);
            return key;
        }, true);
        #endregion

        #region common
        /// <summary>
        /// loads the bookmarks
        /// </summary>
        public GameFileLoader BookmarkLoader { get; } = new GameFileLoader("common/bookmarks.txt");

        /// <summary>
        /// the building loader
        /// </summary>
        public GameFileLoader BuildingLoader { get; } = new GameFileLoader("common/buildings.txt");

        /// <summary>
        /// the cb loader
        /// </summary>
        public GameFileLoader CbLoader { get; } = new GameFileLoader("common/cb_types.txt");

        /// <summary>
        /// the color loader
        /// </summary>
        public GameFileLoader CotColorLoader { get; } = new GameFileLoader("common/cot_colors.txt");

        /// <summary>
        /// the country loader
        /// </summary>
        public GameFileLoader CountryLoader { get; } = new GameFileLoader("common/countries.txt");

        /// <summary>
        /// the country colors loader
        /// </summary>
        public GameFileLoader CountryColorsLoader { get; } = new GameFileLoader("common/country_colors.txt");

        /// <summary>
        /// the crime loader
        /// </summary>
        public GameFileLoader CrimeLoader { get; } = new GameFileLoader("common/crime.txt");
        
        /// <summary>
        /// the culture loader
        /// </summary>
        public GameFileLoader CultureLoader { get; } = new GameFileLoader("common/cultures.txt");

        /// <summary>
        /// the defines loader
        /// </summary>
        public GameFileLoader DefinesLoader { get; } = new GameFileLoader("common/defines.lua");

        /// <summary>
        /// event modifiers loader
        /// </summary>
        public GameFileLoader EventModifiersLoader { get; } = new GameFileLoader("common/event_modifiers.txt");


        /// <summary>
        /// the goods loader
        /// </summary>
        public GameFileLoader GoodsLoader { get; } = new GameFileLoader("common/goods.txt");
        
        /// <summary>
        /// the governments loader
        /// </summary>
        public GameFileLoader GovernmentsLoader { get; } = new GameFileLoader("common/governments.txt");

        /// <summary>
        /// the graphical culture loader
        /// </summary>
        public GameFileLoader GraphicalCultureLoader { get; } = new GameFileLoader("common/graphicalculturetype.txt");

        /// <summary>
        /// the ideologies loader
        /// </summary>
        public GameFileLoader IdeologyLoader { get; } = new GameFileLoader("common/ideologies.txt");
        /// <summary>
        /// the issue loader
        /// </summary>
        public GameFileLoader IssueLoader { get; } = new GameFileLoader("common/issues.txt");

        /// <summary>
        /// national focus
        /// </summary>
        public GameFileLoader NationalFocusLoader { get; } = new GameFileLoader("common/national_focus.txt");

        /// <summary>
        /// the national values loader
        /// </summary>
        public GameFileLoader NationalValueLoader { get; } = new GameFileLoader("common/nationalvalues.txt");

        /// <summary>
        /// the on actions loader
        /// </summary>
        public GameFileLoader OnActionsLoader { get; } = new GameFileLoader("common/on_actions.txt");

        /// <summary>
        /// the pop types loader
        /// </summary>
        public GameFileLoader PopTypeGlobalPromotionLoader { get; } = new GameFileLoader("common/pop_types.txt");
        
        /// <summary>
        /// the production type loader
        /// </summary>
        public GameFileLoader ProductionTypeLoader { get; } = new GameFileLoader("common/production_types.txt");

        /// <summary>
        /// the rebel types loader
        /// </summary>
        public GameFileLoader RebelTypeLoader { get; } = new GameFileLoader("common/rebel_types.txt");

        /// <summary>
        /// the religion loader
        /// </summary>
        public GameFileLoader ReligionLoader { get; } = new GameFileLoader("common/religion.txt");

        /// <summary>
        /// the static modifiers loader
        /// </summary>
        public GameFileLoader StaticModifiersLoader { get; } = new GameFileLoader("common/static_modifiers.txt");

        /// <summary>
        /// the tech school loader
        /// </summary>
        public GameFileLoader TechSchoolLoader { get; } = new GameFileLoader("common/technology.txt");

        /// <summary>
        /// the traits loader
        /// </summary>
        public GameFileLoader TraitsLoader { get; } = new GameFileLoader("common/technology.txt");

        /// <summary>
        /// the triggered modifiers loader
        /// </summary>
        public GameFileLoader TriggeredModifiersLoader { get; } = new GameFileLoader("common/triggered_modifiers.txt");
        #endregion


        public async Task Load()
        {
            await Task.WhenAll(IssueLoader.Load(), 
                DecisionsLoader.Load(),
                EventsLoader.Load(),
                InventionsLoader.Load(),
                TechnologiesLoader.Load(),
                PopLoader.Load(),
                UnitLoader.Load(),
                ProvinceLoader.Load(),
                PopTypeLoader.Load(),
                DiplomacyLoader.Load(),
                OobLoader.Load(),
                BookmarkLoader.Load(),
                BuildingLoader.Load(),
                CbLoader.Load(),
                CotColorLoader.Load(),
                NationalFocusLoader.Load(),
                GraphicalCultureLoader.Load(),
                IdeologyLoader.Load(),
                CountryLoader.Load(),
                CountryColorsLoader.Load(),
                CrimeLoader.Load(),
                CultureLoader.Load(),
                DefinesLoader.Load(),
                EventModifiersLoader.Load(),
                GoodsLoader.Load(),
                GovernmentsLoader.Load(),
                NationalValueLoader.Load(),
                OnActionsLoader.Load(),
                PopTypeLoader.Load(),
                ProductionTypeLoader.Load(),
                RebelTypeLoader.Load(),
                ReligionLoader.Load(),
                StaticModifiersLoader.Load(),
                TechSchoolLoader.Load(),
                TraitsLoader.Load(),
                TriggeredModifiersLoader.Load()
                );
        }
    }
}
