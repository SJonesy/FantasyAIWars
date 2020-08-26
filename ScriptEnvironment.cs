using Neo.IronLua;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FantasyAIWars
{
    class ScriptEnvironment : IDisposable
    {
        private List<Party> parties;
        private Lua lua;
        private dynamic env;
        private int tick;

        public ScriptEnvironment(List<Party> parties, int tick)
        {
            this.parties = parties;
            this.tick = tick;
            lua = new Lua();
            env = lua.CreateEnvironment(); // Create a environment
        }

        public Action DecideAction(Character actor)
        {
            Party friendlyParty = new Party();
            Party enemyParty = new Party();

            foreach (var party in this.parties)
            {
                if (party.Characters.Contains(actor))
                    friendlyParty = party;
                else
                    enemyParty = party;
            }

            // Run Lua Script
            env.enemyParty = new LuaTable();
            env.friendlyParty = new LuaTable();
            env.actor = actor;
            for (int i = 0; i < enemyParty.Characters.Count; i++)
                env.enemyParty[i] = enemyParty.Characters[i];
            for (int i = 0; i < friendlyParty.Characters.Count; i++)
                env.friendlyParty[i] = friendlyParty.Characters[i];
            env.action = "";
            env.dochunk(actor.Script);

            // Parse result and build action
            string actionString = env.action;
            string[] parsedActions = actionString.Split(',');
            Debug.WriteLine("Debug: actionString {0}", actionString);
            if (parsedActions.Length == 1)
            {
                return CreateAction(actor, parsedActions[0], friendlyParty, enemyParty);
            }
            else if (parsedActions.Length == 2)
            {
                Debug.WriteLine("Debug: doing action {0} with target {1}", parsedActions[0].Trim(), parsedActions[1].Trim());
                return CreateAction(actor, parsedActions[0].Trim(), friendlyParty, enemyParty, parsedActions[1].Trim());
            }

            Debug.WriteLine("Debug: {0} using no skill");
            return null;
        }

        /* target should be one of the following:
         * enemyParty
         * friendlyParty
         * enemy [#]
         * friend [#]
         */
        private Action CreateAction(Character actor, string abilityName, Party friendlyParty, Party enemyParty, string target="")
        {
            Ability ability = null;
            foreach (var abil in actor.Abilities)
            {
                if (abil.Type == AbilityType.Passive)
                    continue;

                if (abil.Name.lower() == abilityName.lower())
                {
                    ability = abil;
                    break;
                }
            }

            if (ability == null)
                return null;

            Debug.WriteLine("Debug: {0} is creating action with ability {1}", actor.Name, ability.Name);

            if (!String.IsNullOrEmpty(target))
            {
                target = target.lower();
                if (target == "enemyparty")
                    return new Action(ability, actor, enemyParty);
                else if (target == "friendlyparty")
                    return new Action(ability, actor, friendlyParty);
                else if (target.StartsWith("enemy "))
                    return new Action(ability, actor, enemyParty.Characters[Int32.Parse(target.Split(' ')[1])]);
                else if (target.StartsWith("friend "))
                    return new Action(ability, actor, friendlyParty.Characters[Int32.Parse(target.Split(' ')[1])]);
            }
            
            // Abilities without targets
            return new Action(ability, actor);
        }

        public void Dispose()
        {
            this.lua.Dispose();
        }
    }
}
