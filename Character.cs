using Neo.IronLua;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using YamlDotNet.Serialization;

namespace FantasyAIWars
{
    class Character
    {
        // Loaded from YAML
        public string Name { get; set; }
        public Race Race { get; set; }
        public List<Ability> Abilities { get; set; }
        public string Script { get; set; }

        // Status
        public int MaxHitPoints = 70;
        public int HitPoints    = 0;
        public int MaxMana      = 20;
        public int Mana         = 0;

        // Stats
        public int Strength     = 10;
        public int Dexterity    = 10;
        public int Intelligence = 10;
        public int Wisdom       = 10;
        public int Charisma     = 10;
        public int Constitution = 10;

        // Resists
        public float IceResist    = 1.0f;
        public float FireResist   = 1.0f;
        public float PoisonResist = 1.0f;
        public float HolyResist   = 1.0f;
        public float UnholyResist = 1.0f;
        public float WaterResist  = 1.0f;
        public float AirResist    = 1.0f;
        public float EarthResist  = 1.0f;

        // Combat Variables
        public bool IsCasting      = false;
        public bool IsUsingAbility = false;
        public bool IsAlive        = true;
        public bool IsPoisoned     = false;
        public bool IsRegenerating = false;
        public bool IsRaging       = false;
        public int RegenValue             = 0;
        public int PoisonValue            = 0;
        public int RecoveryTurnsRemaining = 0;
        public Ability AbilityInUse = null;


        public Character() { }

        public bool Init()
        {
            ApplyRace();
            MaxHitPoints += Constitution * 3; // TODO: Am I happy with this?
            HitPoints = MaxHitPoints;
            return true;
        }

        private void ApplyRace()
        {
            switch (Race.Name)
            {
                case "Human":
                    Strength += 2;
                    Intelligence += 2;
                    Dexterity += 2;
                    Constitution += 2;
                    Charisma += 2;
                    Wisdom += 2;
                    break;
            }
        }

        public void DumpCharacterInfo()
        {
            Console.WriteLine("{0} ({1}/{2}hp {3}/{4}mp)\tBuild: {5} [{6}]", 
                Name, HitPoints, MaxHitPoints, Mana, MaxMana, Race.ToString(), string.Join(", ", Abilities));
        }

        public Action DecideAction(List<Party> parties)
        {
            /* TODO: This is just here to remind me that I added LUA scripting support..
             * my plan is to load the LUA environment with data and functions, and that's
             * how I'll expose data to the LUA instead of needing any calls into the C#.
            using (Lua lua = new Lua()) 
            {
                dynamic env = lua.CreateEnvironment(); // Create a environment
                env.dochunk("a = 'Hallo World!';", "test.lua"); // Create a variable in Lua
                Console.WriteLine(env.a); // Access a variable in C#
                env.dochunk("function add(b) return b + 3; end;", "test.lua"); // Create a function in Lua
                Console.WriteLine("Add(3) = {0}", env.add(3)); // Call the function in C#
            } */
            return null;
        }
    }
}
