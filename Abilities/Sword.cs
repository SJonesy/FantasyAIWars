using System;
using Console = Colorful.Console;

namespace FantasyAIWars.Abilities
{
    class Sword : MeleeAbility
    {
        public override string Name { get; }
        public override int Delay { get; }
        public override int Cooldown { get; }
        public override AbilityType Type { get; }
        public override DamageType DamageType { get; }
        public override int EngagedDelay { get; }

        public Sword()
        {
            Name = "Sword";
            Delay = 6;
            EngagedDelay = 3;
            Cooldown = 3;
            Type = AbilityType.Melee;
            DamageType = DamageType.Physical;
        }

        public override void DoAbility(Action action)
        {
            int maxDamage = action.Actor.Stats.Strength + action.Actor.Stats.Dexterity + 10;
            int damage = action.Actor.Random.Next(10, maxDamage);
            int modifiedDamage = ModifyDamage(damage, action.TargetCharacter);
            DoDamage(action.Actor, action.TargetCharacter, modifiedDamage, true);

            string output = String.Format("{0} slashes {1} with his sword for {2} damage.", action.Actor.Name, action.TargetCharacter.Name, modifiedDamage);
            Console.WriteLine(output);
        }
    }
}
