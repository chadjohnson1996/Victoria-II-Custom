using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria_II_Custom_Lib.ActionEffects;
using Victoria_II_Custom_Lib.Countries;
using Victoria_II_Custom_Lib.Events;
using Victoria_II_Custom_Lib.FileLoader.Loaders;
using Victoria_II_Custom_Lib.Goods;
using Victoria_II_Custom_Lib.LocalizationInfo;

namespace Victoria_II_Custom_Lib
{
    public class GameState
    {
        public GlobalMetadata GlobalMetadata { get; } = new GlobalMetadata();
        /// <summary>
        /// the localization
        /// </summary>
        public Localization Localization { get; } = new Localization();
        /// <summary>
        /// the loaders
        /// </summary>
        public GameStateLoaders Loaders { get; } = new GameStateLoaders();



        /// <summary>
        /// the good state
        /// </summary>
        public GoodState GoodState { get; private set; }

        /// <summary>
        /// the countries
        /// </summary>
        public ConcurrentDictionary<string, Country> Countries { get; private set; }

        /// <summary>
        /// the event store
        /// </summary>
        public EventStore EventStore { get; set; } = new EventStore();


        /// <summary>
        /// runs a game tick
        /// </summary>
        public void Tick()
        {
            GlobalMetadata.ActionQueue.Decrement();
            GlobalMetadata.Date = GlobalMetadata.Date.AddDays(1);
        }
        public async Task Init()
        {
            await Loaders.Load();
            await Localization.Init();
            GoodState = new GoodState(await Loaders.GoodsLoader.Load());
            Countries = Country.GetAllCountries(this);
        }
    }
}
