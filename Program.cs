using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FantasyAIWars
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load Parties
            List<Party> parties = new List<Party>();
            foreach (var arg in args)
            {
                string input = System.IO.File.ReadAllText(arg);
                var deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
                parties.Add(deserializer.Deserialize<Party>(input));
            }
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

            List<List<Action>> queuedActions = new List<List<Action>>();

            // Main Combat Loop
            for (int tick = 0; tick < TICK_LIMIT; tick++)
            {
                if (tick % 20 == 0) {
                    // DisplayStatus(parties);
                }

                // Do queued actions
                foreach (var action in queuedActions[tick])
                {
                    if (!action.Actor.IsAlive)
                        continue;
                    
                    // TODO: actually do actions

                    action.Actor.RecoveryTurnsRemaining = 0;
                    action.Actor.IsUsingAbility = false;
                    action.Actor.AbilityInUse = Ability.Idle;
                }

                // Queue new actions
                foreach (var party in parties)
                {
                    foreach (var character in party.Characters)
                    {
                        if (!character.IsAlive || character.IsUsingAbility || character.IsCasting)
                            continue;

                        if (character.RecoveryTurnsRemaining > 0)
                        {
                            character.RecoveryTurnsRemaining--;
                            continue;
                        }

                        Action action = character.DecideAction(parties);
                        if (action != null)
                        {
                            int nextAction = tick + (int)Math.Round(action.GetDelay() * (10.0 / action.Actor.Dexterity));
                            if (nextAction <= tick)
                                nextAction = tick + 1;
                            queuedActions[nextAction].Add(action);
                            action.Actor.IsUsingAbility = true;
                            action.Actor.AbilityInUse = action.Ability;
                        }
                    }
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
