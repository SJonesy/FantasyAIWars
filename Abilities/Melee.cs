using System;
using System.Drawing;
using Console = Colorful.Console;

namespace FantasyAIWars.Abilities
{
    class Melee : Ability
    {
        public override string Name { get; }
        public override int Delay { get; }
        public override int Cooldown { get; }
        public override AbilityType Type { get; }
        public override DamageType DamageType { get; }

        public Melee()
        {
            Name = "Melee";
            Delay = 1;
            Cooldown = 5;
            Type = AbilityType.Attack;
            DamageType = DamageType.Physical;
        }

        public override void DoAbility(Action action)
        {
            System.Random random = new System.Random();
            int damage = random.Next(1, 6);
            damage += action.Actor.Stats.Strength - 10;
            DoDamage(action.Actor, action.TargetCharacter, damage);
            string output = String.Format("{0} punches {1} right in the mouth for {2} damage.", action.Actor.Name, action.TargetCharacter.Name, damage);
            Console.WriteLine(output, Color.Gray);
        }
    }
}
