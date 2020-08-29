using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Console = Colorful.Console;

namespace FantasyAIWars.Abilities
{
    class BurstHeal : SpellAbility
    {
        public override string Name { get; }
        public override int Delay { get; }
        public override int Cooldown { get; }
        public override AbilityType Type { get; }
        public override string OutputText { get; }
        public override Color OutputColor { get; }

        public BurstHeal()
        {
            Name = "BurstHeal";
            Delay = 1;
            Cooldown = 10;
            ManaCost = 10;
            Type = AbilityType.Heal;
            OutputText = "A burst of heavenly light engulfs {target}, as {actor}'s burst heal heals them for {damage}hp.";
            OutputColor = Color.Yellow;
        }

        public override void DoAbility(Action action)
        {
            // (20 to 30) + (1 to Wisdom)
            int healing = action.Actor.Random.Next(20, 30) + (int)(action.Actor.Stats.Wisdom * action.Actor.Random.NextDouble());
            action.TargetCharacter.DoHealing(action, healing);
        }
    }
}
