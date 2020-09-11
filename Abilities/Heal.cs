using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Console = Colorful.Console;

namespace FantasyAIWars.Abilities
{
    class Heal : SpellAbility
    {
        public override string Name { get; }
        public override int Delay { get; }
        public override int Cooldown { get; }
        public override AbilityType Type { get; }
        public override string OutputText { get; }
        public override Color OutputColor { get; }

        public Heal()
        {
            Name = "Heal";
            Delay = 3;
            Cooldown = 2;
            ManaCost = 3;
            Type = AbilityType.Heal;
            OutputText = "{target}'s body is covered in a healing light as {actor} finishes casting heal for {healing}hp.";
            OutputColor = Color.PaleGoldenrod;
        }

        public override void DoAbility(Action action)
        {
            // (10 to 15) + (1 to Wisdom)
            int healing = action.Actor.Random.Next(10, 15) + (int)(action.Actor.Stats.Wisdom * action.Actor.Random.NextDouble());
            action.TargetCharacter.DoHealing(action, healing);
        }
    }
}
