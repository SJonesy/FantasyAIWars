using System;
using System.Collections.Generic;

namespace FantasyAIWars
{
    class Character
    {
        // Loaded from YAML
        public string Name { get; set; }
        public Race Race { get; set; }
        public List<Ability> Abilities { get; set; }
        public string Script { get; set; }

        // For LuA Scripting
        public int CharacterIndex { get; set; }
        public int PartyIndex { get; set; }

        // Status
        public int MaxHitPoints = 70;
        public int HitPoints = 0;
        public int MaxMana = 20;
        public int Mana = 0;

        // Stats
        public StatBlock Stats;

        // Resists
        public ResistBlock Resists;

        // Combat Variables
        public bool IsCasting = false;
        public bool IsUsingAbility = false;
        public bool IsAlive = true;
        public bool IsPoisoned = false;
        public bool IsRegenerating = false;
        public bool IsRaging = false;
        public int RegenValue = 0;
        public int PoisonValue = 0;
        public int RecoveryTurnsRemaining = 0;
        public Ability AbilityInUse = null;
        public Character EngagedWith = null;
        public Character IsGuardedBy = null;

        public Character() { }

        public bool Init(int partyIndex, int characterIndex)
        {
            PartyIndex = partyIndex;
            CharacterIndex = characterIndex;
            Race.Apply(this);
            ApplyPassives();
            MaxHitPoints += Stats.Constitution * 3; // TODO: Am I happy with this?
            HitPoints = MaxHitPoints;
            return true;
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
    }
}
