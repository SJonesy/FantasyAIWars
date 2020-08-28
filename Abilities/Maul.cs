using FantasyAIWars.Races;
using System;
using Console = Colorful.Console;

namespace FantasyAIWars.Abilities
{
    class Maul : MeleeAbility
    {
        public override string Name { get; }
        public override int Delay { get; }
        public override int Cooldown { get; }
        public override AbilityType Type { get; }
        public override DamageType DamageType { get; }
        public override int EngagedDelay { get; }

        public Maul()
        {
            Name = "Maul";
            Delay = 10;
            EngagedDelay = 3;
            Cooldown = 5;
            Type = AbilityType.Melee;
            DamageType = DamageType.Physical;
        }

        public override void DoAbility(Action action)
        {
            int damage = action.Actor.Random.Next(10, action.Actor.Stats.Strength * 4);

            if (action.TargetCharacter.Race.GetType() == typeof(Skeleton))
                damage *= 5;
            int modifiedDamage = ModifyDamage(damage, action.TargetCharacter);
            DoDamage(action.Actor, action.TargetCharacter, modifiedDamage, true);

            string output = String.Format("{0} SLAMS {1} with a huge maul for {2} damage.", action.Actor.Name, action.TargetCharacter.Name, modifiedDamage);
            Console.WriteLine(output);
        }
    }
}
