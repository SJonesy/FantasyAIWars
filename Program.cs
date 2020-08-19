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
                var input = System.IO.File.ReadAllText(arg);
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

            List<List<CombatAction>> queuedActions = new List<List<CombatAction>>();
            Dictionary<int, List<CombatAction>> actionMap = new Dictionary<int, List<CombatAction>>();

            // Main Combat Loop
            for (int tick = 0; tick < TICK_LIMIT; tick++)
            {
                if (tick % 20 == 0)
                    DisplayStatus(parties);
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
