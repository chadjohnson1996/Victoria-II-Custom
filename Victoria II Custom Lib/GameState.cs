using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria_II_Custom_Lib.FileLoader.Loaders;
using Victoria_II_Custom_Lib.Goods;
using Victoria_II_Custom_Lib.LocalizationInfo;

namespace Victoria_II_Custom_Lib
{
    public class GameState
    {
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

        public async Task Init()
        {
            await Loaders.Load();
            await Localization.Init();
            GoodState = new GoodState(await Loaders.GoodsLoader.Load());
        }
    }
}
