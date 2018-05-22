using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria_II_Custom_Lib.Extensions;

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
            Handlers["and"] = AndHandler;
            Handlers["or"] = OrHandler;
            Handlers["not"] = NotHandler;
            Handlers["year"] = YearHandler;
            Handlers["month"] = MonthHandler;
            Handlers["check_variable"] = CheckVariable;
            Handlers["has_global_flag"] = HasGlobalFlag;
            Handlers["is_canal_enabled"] = IsCanalEnabled;
            Handlers["administration_spending"] = AdminSpending;
            Handlers["ai"] = Ai;
            Handlers["alliance_with"] = AllianceWith;
            Handlers["average_militancy"] = AverageMilitancy;
            Handlers["average_consciousness"] = AverageConciousness;
            Handlers["badboy"] = Badboy;
        }

        /// <summary>
        /// root method to eval condition
        /// </summary>
        /// <param name="scope">the scope</param>
        /// <param name="root">the root</param>
        /// <returns></returns>
        public bool EvalCondition(Scope scope, KeyValueNode root)
        {
            return Handlers[root.Key.ToLowerInvariant()](scope, root);
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

        /// <summary>
        /// handles logical or
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="root"></param>
        /// <returns></returns>
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

        /// <summary>
        /// handles logical not
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        private bool NotHandler(Scope scope, KeyValueNode root)
        {
            foreach (var child in root)
            {
                return !EvalCondition(scope, child);
            }

            throw new ApplicationException("A 'NOT' Expression must have a body");
        }

        /// <summary>
        /// checks if current year is >= condition
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        private bool YearHandler(Scope scope, KeyValueNode root)
        {
            return scope.State.GlobalMetadata.Date.Year >= root.Value.AsInt();
        }

        /// <summary>
        /// returns true if the current month is less than or equal to parameter month
        /// note the month must be the string representation
        /// (e.g. January instead of 1)
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        private bool MonthHandler(Scope scope, KeyValueNode root)
        {
            return scope.State.GlobalMetadata.MonthMap[root.Value] <= scope.State.GlobalMetadata.Date.Month;
        }

        /// <summary>
        /// returns true if the variable specified in the "which" clause is set and equal to the "value"
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        private bool CheckVariable(Scope scope, KeyValueNode root)
        {
            var variableInfo = scope.State.GlobalMetadata.Variables;
            var name = root["which"].Value;
            var value = root["value"].Value.AsDecimal();
            return variableInfo.ContainsKey(name) && variableInfo[name] == value;
        }

        /// <summary>
        /// returns true if the specified global flag is set
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        private bool HasGlobalFlag(Scope scope, KeyValueNode root)
        {
            return scope.State.GlobalMetadata.GlobalFlags.Contains(root.Value);
        }

        /// <summary>
        /// returns true if the given canal is enabled
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        private bool IsCanalEnabled(Scope scope, KeyValueNode root)
        {
            var canal = root.Value.AsInt();
            return scope.State.GlobalMetadata.EnabledCanals.Contains(canal);
        }

        /// <summary>
        /// returns true if the country's admin spending is greater than or equal to specified value
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        private bool AdminSpending(Scope scope, KeyValueNode root)
        {
            return scope.Country.BudgetInfo.AdminSpending >= root.Value.AsDecimal();
        }

        /// <summary>
        /// return true if the country is the ai value specified in the value
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        private bool Ai(Scope scope, KeyValueNode root)
        {
            return scope.Country.IsAi == root.Value.AsBool();
        }

        /// <summary>
        /// return true if the country is allied with the value
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        private bool AllianceWith(Scope scope, KeyValueNode root)
        {
            var value = root.Value.ToUpperInvariant();
            var allies = scope.Country.DiplomaticInfo.Allies;
            switch (value)
            {
                case "THIS":
                    return allies.ContainsKey(scope.This.Country.Tag);
                case "FROM":
                    return allies.ContainsKey(scope.Previous.Country.Tag);
                default:
                    return allies.ContainsKey(value);
            }
        }

        /// <summary>
        /// returns true if the conciousness in a province is >= a value
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        private bool AverageConciousness(Scope scope, KeyValueNode root)
        {
            var value = root.Value.AsDecimal();
            if (scope.Province != null)
            {
                return scope.Province.Consciousness >= value;
            }
            else
            {
                return scope.Country.Consciousness >= value;
            }
        }
        /// <summary>
        /// returns true if the average militancy is >= the value
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        private bool AverageMilitancy(Scope scope, KeyValueNode root)
        {
            var value = root.Value.AsDecimal();
            if (scope.Province != null)
            {
                return scope.Province.Militancy >= value;
            }
            else
            {
                return scope.Country.Militancy >= value;
            }
        }

        /// <summary>
        /// returns true if the given country has more than x% of the infamy limit
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        private bool Badboy(Scope scope, KeyValueNode root)
        {
            var value = root.Value.AsDecimal();
            var scopeValue = scope.Country.DiplomaticInfo.Infamy / scope.State.GlobalMetadata.InfamyLimit;
            return scopeValue >= value;
        }
    }
}
