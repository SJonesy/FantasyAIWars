using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FantasyAIWars
{
    class Program
    {
        static int Main(string[] args)
        {
            const int TICK_LIMIT = 10000;

            // Initialize Parties
            List<Party> parties = new List<Party>();
            foreach (var arg in args)
            {
                var input = System.IO.File.ReadAllText(arg);
                var deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
                parties.Add(deserializer.Deserialize<Party>(input));
            }
            if (parties.Count != 2)
            {
                Console.WriteLine("Paramaters should be exactly 2 party files.");
                return 1;
            }
            foreach (var party in parties)
            {
                Console.WriteLine("Initializing party: {0}", party.Name);
                foreach (var character in party.Characters)
                {
                    character.Init();
                }
                Console.WriteLine();
            }

            // Main Game Loop
            for (int tick = 0; tick <= TICK_LIMIT; tick++)
            {
            }

            Console.WriteLine("Turn limit exceeded; match was a draw.");
            return 0;
        }
    }
}
