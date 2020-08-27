﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FantasyAIWars
{
    class Program
    {
        const int MAX_ABILITIES = 4;
        const int MAX_PARTY_MEMBERS = 6;

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

            if (!IsValid(parties))
            {
                Console.WriteLine("One or more parties are not valid. Exiting.");
                return;
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

        private static bool IsValid(List<Party> parties)
        {
            foreach (var party in parties)
            {
                if (party.Characters.Count > MAX_PARTY_MEMBERS)
                {
                    Console.WriteLine("Each party may only have {0} members. {1} has {2} members.", MAX_PARTY_MEMBERS, party.Name, party.Characters.Count);
                    return false;
                }

                foreach (var character in party.Characters)
                {
                    if (character.Abilities.Count > MAX_ABILITIES)
                    {
                        Console.WriteLine("Each character may only have {0} abilities. {1} has {2} abilites.", MAX_ABILITIES, character.Name, character.Abilities.Count);
                        return false;
                    }

                    if (character.Abilities.Count() != character.Abilities.Distinct().Count())
                    {
                        Console.WriteLine("Character abilities must be unique. {1} has duplicate abilites.", character.Name);
                        return false;
                    }
                }
            }

            return true;
        }

        private static void Duel(List<Party> parties)
        {
            const int TICK_LIMIT = 1000;

            for (int i = 0; i < parties.Count; i++)
                for (int j = 0; j < parties[i].Characters.Count; j++)
                    parties[i].Characters[j].Init(i, j);

            List<Action>[] queuedActions = new List<Action>[TICK_LIMIT];
            for (int i = 0; i < TICK_LIMIT; i++)
                queuedActions[i] = new List<Action>();

            // Main Combat Loop
            for (int tick = 0; tick < TICK_LIMIT; tick++)
            {
                //Uncomment for tick debugging
                //Console.Write("{0}: ", tick.ToString());

                if (tick % 20 == 0)
                {
                    DisplayStatus(parties);
                }

                // Do queued actions
                // TODO: order actions by actors' dex
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
                            int nextAction = tick + (int)Math.Round(action.GetDelay() * (10.0 / action.Actor.Stats.Dexterity));
                            if (nextAction <= tick)
                                nextAction = tick + 1;
                            if (nextAction >= TICK_LIMIT)
                                continue;
                            action.Ability.OnQueue(action);
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
            Console.WriteLine();
            Console.WriteLine("============================================================================");
            foreach (var party in parties)
            {
                Console.WriteLine("Party: {0}", party.Name);
                foreach (var character in party.Characters)
                {
                    character.DumpCharacterInfo();
                }
            }
            Console.WriteLine("============================================================================");
            Console.WriteLine();
        }

        private static void Brawl(List<Party> parties)
        {
            Console.WriteLine("Brawl mode (more than 2 parties) is not currently implemented.");
        }
    }
}
