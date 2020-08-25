using FantasyAIWars.Abilities;
using System;
using System.Collections.Generic;
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
            var listOfAbilities = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                                   from assemblyType in domainAssembly.GetTypes()
                                   where assemblyType.IsSubclassOf(typeof(Ability)) && !assemblyType.IsAbstract
                                   select assemblyType)
                                   .ToArray();
            var deserializerBuilder = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance);
            foreach (var ability in listOfAbilities)
            {
                deserializerBuilder.WithTagMapping("!" + ability.Name, ability);
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
                    action.Actor.AbilityInUse = null;
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
