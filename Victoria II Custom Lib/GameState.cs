using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria_II_Custom_Lib.ActionEffects;
using Victoria_II_Custom_Lib.Events;
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
        /// the global flags
        /// </summary>
        public HashSet<string> GlobalFlags { get; } = new HashSet<string>();

        /// <summary>
        /// the variables
        /// </summary>
        public Dictionary<string, decimal> Variables { get; } = new Dictionary<string, decimal>();

        /// <summary>
        /// the good state
        /// </summary>
        public GoodState GoodState { get; private set; }

        /// <summary>
        /// the action queue
        /// </summary>
        public ActionQueue ActionQueue { get; set; } = new ActionQueue();

        /// <summary>
        /// the event store
        /// </summary>
        public EventStore EventStore { get; set; } = new EventStore();

        public DateTime Date { get; set; } = DateTime.MinValue;

        /// <summary>
        /// runs a game tick
        /// </summary>
        public void Tick()
        {
            ActionQueue.Decrement();
            Date = Date.AddDays(1);
        }
        public async Task Init()
        {
            await Loaders.Load();
            await Localization.Init();
            GoodState = new GoodState(await Loaders.GoodsLoader.Load());
        }

        /// <summary>
        /// sets a flag from the global flags
        /// </summary>
        /// <param name="flag">the flags to set</param>
        public void SetFlag(string flag)
        {
            GlobalFlags.Add(flag);
        }

        /// <summary>
        /// removes a flag from the global flag
        /// </summary>
        /// <param name="flag">the flag</param>
        public void RemoveFlag(string flag)
        {
            GlobalFlags.Remove(flag);
        }

        /// <summary>
        /// sets a variable
        /// </summary>
        /// <param name="name">the variable to set</param>
        /// <param name="value">the value</param>
        public void SetVariable(string name, decimal value)
        {
            Variables[name] = value;
        }

        /// <summary>
        /// adds the given value to the variable
        /// </summary>
        /// <param name="name">the variable</param>
        /// <param name="toAdd">the value to add</param>
        public void ChangeVariable(string name, decimal toAdd)
        {
            Variables[name] += toAdd;
        }
    }
}
