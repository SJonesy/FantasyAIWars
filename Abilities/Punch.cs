using System;
using System.Drawing;
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
        public override int EngagedDelay { get; }
        public override string OutputText { get; }
        public override Color OutputColor { get; }

        public Punch()
        {
            Name = "Punch";
            Delay = 8;
            EngagedDelay = 2;
            Cooldown = 1;
            Type = AbilityType.Melee;
            DamageType = DamageType.Physical;
            OutputText = "{actor} punches {target} right in the mouth for {damage}hp.";
            OutputColor = Color.LightGray;
        }

        public override void DoAbility(Action action)
        {
            int damage = action.Actor.Random.Next(1, 5);
            damage += action.Actor.Stats.Strength - 12;

            action.TargetCharacter.DoDamage(action, damage);
        }
    }
}
