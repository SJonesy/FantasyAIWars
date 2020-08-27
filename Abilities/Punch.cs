using System;
using Console = Colorful.Console;

namespace FantasyAIWars.Abilities
{
    class Punch : MeleeAbility
    {
        public override string Name { get; }
        public override int Delay { get; }
        public override int Cooldown { get; }
        public override AbilityType Type { get; }
        public override DamageType DamageType { get; }

        public Punch()
        {
            Name = "Punch";
            Delay = 7;
            Cooldown = 2;
            Type = AbilityType.Melee;
            DamageType = DamageType.Physical;
        }

        public override void DoAbility(Action action)
        {
            System.Random random = new System.Random();
            int damage = random.Next(1, 6);
            damage += action.Actor.Stats.Strength - 10;
            int modifiedDamage = ModifyDamage(damage, action.TargetCharacter);
            DoDamage(action.Actor, action.TargetCharacter, modifiedDamage, true);

            string output = String.Format("{0} punches {1} right in the mouth for {2} damage.", action.Actor.Name, action.TargetCharacter.Name, modifiedDamage);
            Console.WriteLine(output);
        }
    }
}
