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

        public BurstHeal()
        {
            Name = "BurstHeal";
            Delay = 1;
            Cooldown = 10;
            ManaCost = 10;
            Type = AbilityType.Heal;
        }

        public override void DoAbility(Action action)
        {
            // (30 to 40) + (1 to Wisdom)
            int healing = action.Actor.Random.Next(30, 40) + (int)(action.Actor.Stats.Wisdom * action.Actor.Random.NextDouble());
            string output = String.Format("A burst of heavenly light engulfs {0}, as {1}'s burst heal heals them for {2}hp.",
                action.TargetCharacter.Name, action.Actor.Name, healing);
            DoHealing(action.Actor, action.TargetCharacter, healing);
            Console.WriteLine(output, Color.Yellow);
        }
    }
}
