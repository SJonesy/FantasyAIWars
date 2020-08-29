using System;
using System.Collections.Generic;
using System.Drawing;
using Console = Colorful.Console;

namespace FantasyAIWars
{
    class Character
    {
        // Loaded from YAML
        public string Name { get; set; }
        public Race Race { get; set; }
        public List<Ability> Abilities { get; set; }
        public string Script { get; set; }

        // Internal/Scripting
        public int CharacterIndex { get; set; }
        public int PartyIndex { get; set; }
        public Random Random { get; set; }

        // Status
        public int MaxHitPoints = 70;
        public int HitPoints = 0;
        public int MaxMana = 60;
        public int Mana = 0;

        // Stats
        public StatBlock Stats;

        // Resists
        public ResistBlock Resists;

        // Immunities
        public bool ImmuneToFear = false;
        public bool ImmuneToParalzye = false;
        public bool ImmuneToSleep = false;

        // Combat Variables
        public bool IsCasting = false;
        public bool IsUsingAbility = false;
        public bool IsAlive = true;
        public bool IsPoisoned = false;
        public bool IsRegenerating = false;
        public bool IsRaging = false;
        public bool Interrupted = false;
        public int RegenValue = 0;
        public int PoisonValue = 0;
        public int RecoveryTurnsRemaining = 0;
        public Ability AbilityInUse = null;
        public Character EngagedWith = null;
        public Character IsGuardedBy = null;

        public Character() { }

        public bool Init(int partyIndex, int characterIndex, Random random)
        {
            PartyIndex = partyIndex;
            CharacterIndex = characterIndex;
            Random = random;
            Race.Apply(this);
            ApplyPassives();
            MaxHitPoints += Stats.Constitution * 3; // TODO: Am I happy with this?
            HitPoints = MaxHitPoints;
            MaxMana += Stats.Intelligence * 4;
            Mana = MaxMana;
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

        public void DoDamage(Action action, int damage, bool disableOutput = false)
        {
            damage = ApplyResists(damage, action.Ability.DamageType);
            HitPoints -= (int)damage;

            if (!disableOutput)
                action.Ability.DoOutput(action, damage, this);
        }

        public void DoHealing(Action action, int healing, bool disableOutput = false)
        {
            HitPoints = Math.Min(MaxHitPoints, HitPoints + healing);

            if (!disableOutput)
                action.Ability.DoOutput(action, healing, this);
        }

        private int ApplyResists(float damage, DamageType damageType)
        {
                 if (damageType == DamageType.Ice) damage /= Resists.Ice;
            else if (damageType == DamageType.Fire) damage /= Resists.Fire;
            else if (damageType == DamageType.Poison) damage /= Resists.Poison;
            else if (damageType == DamageType.Holy) damage /= Resists.Holy;
            else if (damageType == DamageType.Unholy) damage /= Resists.Unholy;
            else if (damageType == DamageType.Water) damage /= Resists.Water;
            else if (damageType == DamageType.Air) damage /= Resists.Air;
            else if (damageType == DamageType.Earth) damage /= Resists.Earth;
            else if (damageType == DamageType.Physical) damage /= Resists.Physical;

            return Math.Max((int)damage, 0);
        }

        public void DumpCharacterInfo()
        {
            Color outputColor = Color.Gray;
            if (!IsAlive)
            {
                outputColor = Color.DarkRed;
            }
            else if (HitPoints < MaxHitPoints)
            {
                if (HitPoints < MaxHitPoints / 2)
                    outputColor = Color.Gray;
                if (HitPoints < MaxHitPoints / 4)
                    outputColor = Color.IndianRed;
            }
            else
            {
                outputColor = Color.LightGray;
            }

            string output = String.Format("{0} ({1}/{2}hp {3}/{4}mp)\tBuild: {5} [{6}]",
                Name, HitPoints, MaxHitPoints, Mana, MaxMana, Race.ToString(), string.Join(", ", Abilities));
            Console.WriteLine(output, outputColor);
        }
    }
}
