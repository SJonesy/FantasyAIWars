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

        // For LUA Scripting
        public int Index { get; set; }

        // Status
        public int MaxHitPoints = 70;
        public int HitPoints    = 0;
        public int MaxMana      = 20;
        public int Mana         = 0;

        // Stats
        public StatBlock Stats;

        // Resists
        public float IceResist      = 1.0f;
        public float FireResist     = 1.0f;
        public float PoisonResist   = 1.0f;
        public float HolyResist     = 1.0f;
        public float UnholyResist   = 1.0f;
        public float WaterResist    = 1.0f;
        public float AirResist      = 1.0f;
        public float EarthResist    = 1.0f;
        public float PhysicalResist = 1.0f;

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

        public bool Init(int index)
        {
            Index = index;
            ApplyRace();
            ApplyPassives();
            MaxHitPoints += Stats.Constitution * 3; // TODO: Am I happy with this?
            HitPoints = MaxHitPoints;
            return true;
        }

        private void ApplyRace()
        {
            Stats = Race.Stats;
        }

        private void ApplyPassives()
        {
            foreach (var ability in Abilities)
            {
                if (ability.Type == AbilityType.Passive)
                {
                    ability.DoAbility(new Action(ability, this));
                }
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
             * how I'll expose data to the LUA instead of needing any calls into the C#. */
            return null;
        }
    }
}
