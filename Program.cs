using FantasyAIWars.Abilities;
using Neo.IronLua;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FantasyAIWars
{
    class Program
    {
        static void Main(string[] args)
        {
            // Build YAML Parser
            var listOfAbilities = (
                from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                from assemblyType in domainAssembly.GetTypes()
                where assemblyType.IsSubclassOf(typeof(Ability)) && !assemblyType.IsAbstract
                select assemblyType
            ).ToArray();
            var listOfRaces = (
                from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                from assemblyType in domainAssembly.GetTypes()
                where assemblyType.IsSubclassOf(typeof(Race)) && !assemblyType.IsAbstract
                select assemblyType
            ).ToArray();
            var deserializerBuilder = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance);
            foreach (var type in listOfAbilities.Concat(listOfRaces))
            {
                deserializerBuilder.WithTagMapping("!" + type.Name, type);
            }
            var deserializer = deserializerBuilder.Build();

            // Load Parties
            List<Party> parties = new List<Party>();
            foreach (var arg in args)
            {
                string input = System.IO.File.ReadAllText(arg);
                parties.Add(deserializer.Deserialize<Party>(input));
            }

            // Pick combat type
            if (parties.Count == 2)
            {
                Duel(parties);
            }
            else if (parties.Count > 2)
            {
                Brawl(parties);
            }
            else
            {
                Console.WriteLine("You must run this with at least 2 parties");
            }
        }
        private static void Duel(List<Party> parties)
        {
            const int TICK_LIMIT = 1000;

            foreach (var party in parties)
                foreach (var character in party.Characters)
                    character.Init();

            DisplayStatus(parties);

            List<Action>[] queuedActions = new List<Action>[TICK_LIMIT];
            for (int i = 0; i < TICK_LIMIT; i++)
                queuedActions[i] = new List<Action>();

            // Main Combat Loop
            for (int tick = 0; tick < TICK_LIMIT; tick++)
            {
                Console.Write("{0}: ", tick.ToString());

                if (tick % 20 == 0) {
                    DisplayStatus(parties);
                }

                // Do queued actions
                foreach (var action in queuedActions[tick])
                {
                    Debug.WriteLine("Debug: {0} doing queued action {1}", action.Actor.Name, action.Ability.Name);

                    if (!action.Actor.IsAlive)
                        continue;

                    action.Ability.DoAbility(action);

                    action.Actor.RecoveryTurnsRemaining = action.Ability.Cooldown;
                    action.Actor.IsUsingAbility = false;
                    action.Actor.AbilityInUse = null;
                }

                // Queue new actions
                using ScriptEnvironment se = new ScriptEnvironment(parties, tick);
                foreach (var party in parties)
                {
                    foreach (var actor in party.Characters)
                    {
                        if (!actor.IsAlive || actor.IsUsingAbility || actor.IsCasting)
                            continue;

                        if (actor.RecoveryTurnsRemaining > 0)
                        {
                            actor.RecoveryTurnsRemaining--;
                            continue;
                        }

                        Action action = se.DecideAction(actor);
                        if (action != null)
                        {
                            // TODO: this seems really powerful, maybe dex is OP here?
                            int nextAction = tick + (int)Math.Round(action.GetDelay() * (10.0 / action.Actor.Stats.Dexterity));
                            if (nextAction <= tick)
                                nextAction = tick + 1;
                            if (nextAction >= TICK_LIMIT)
                                continue;
                            queuedActions[nextAction].Add(action);
                            action.Actor.IsUsingAbility = true;
                            action.Actor.AbilityInUse = action.Ability;
                        }
                    }
                }

                // End of tick effects and cleanup
                foreach (var party in parties)
                {
                    foreach (var character in party.Characters)
                    {
                        if (character.HitPoints <= 0)
                            character.IsAlive = false;
                    }
                }

                // Check for winner
                List<Party> aliveParties = new List<Party>();
                foreach (var party in parties)
                {
                    bool partyIsWiped = true;
                    foreach (var character in party.Characters)
                    {
                        if (character.IsAlive)
                        {
                            partyIsWiped = false;
                            break;
                        }
                    }
                    if (!partyIsWiped)
                        aliveParties.Add(party);
                }
                if (aliveParties.Count == 0)
                {
                    Console.WriteLine("Both parties have died! All characters are dead. The match is a tie.");
                    DisplayStatus(parties);
                    return;
                }
                else if (aliveParties.Count == 1)
                {
                    DisplayStatus(parties);
                    Console.WriteLine("{0} has won!", aliveParties[0].Name);
                    return;
                }


            }

            Console.WriteLine("Tick limit exceeded; match was a draw.");
            return;
        }

        private static void DisplayStatus(List<Party> parties)
        {
            foreach (var party in parties)
            {
                Console.WriteLine("Party: {0}", party.Name);
                foreach (var character in party.Characters)
                {
                    character.DumpCharacterInfo();
                }
                Console.WriteLine();
            }
        }

        private static void Brawl(List<Party> parties) 
        {
            Console.WriteLine("Brawl mode (more than 2 parties) is not currently implemented.");
        }
    }
}
