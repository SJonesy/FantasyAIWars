using FantasyAIWars.Races;
using System;
using System.Drawing;
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
        public override string OutputText { get; }
        public override Color OutputColor { get; }

        public Maul()
        {
            Name = "Maul";
            Delay = 10;
            EngagedDelay = 3;
            Cooldown = 5;
            Type = AbilityType.Melee;
            DamageType = DamageType.Physical;
            OutputText = "{actor} SLAMS {target} with a huge maul for {damage} damage.";
            OutputColor = Color.LightGray;
        }

        public override void DoAbility(Action action)
        {
            int damage = action.Actor.Random.Next(10, action.Actor.Stats.Strength * 3);
            if (action.TargetCharacter.Race.GetType() == typeof(Skeleton))
                damage *= 4;
            action.TargetCharacter.DoDamage(action, damage);
        }
    }
}
