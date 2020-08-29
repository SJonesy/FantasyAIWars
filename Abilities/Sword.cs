using System;
using System.Drawing;
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
        public override string OutputText { get; }
        public override Color OutputColor { get; }

        public Sword()
        {
            Name = "Sword";
            Delay = 6;
            EngagedDelay = 3;
            Cooldown = 3;
            Type = AbilityType.Melee;
            DamageType = DamageType.Physical;
            OutputText = "{actor} slashes {target} with his sword for {damage}hp.";
            OutputColor = Color.LightGray;
        }

        public override void DoAbility(Action action)
        {
            int maxDamage = action.Actor.Stats.Strength + action.Actor.Stats.Dexterity + 5;
            int damage = action.Actor.Random.Next(10, maxDamage);

            action.TargetCharacter.DoDamage(action, damage);
        }
    }
}
