using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FantasyAIWars.Abilities
{
    class MassHeal : SpellAbility
    {
        public override string Name { get; }
        public override int Delay { get; }
        public override int Cooldown { get; }
        public override AbilityType Type { get; }
        public override string OutputText { get; }
        public override Color OutputColor { get; }

        public MassHeal()
        {
            Name = "MassHeal";
            Delay = 7;
            Cooldown = 10;
            ManaCost = 10;
            Type = AbilityType.Heal;
            OutputText = "{target}'s body is covered in a healing light as {actor} finishes casting mass heal for {healing}hp.";
            OutputColor = Color.PaleGoldenrod;
        }

        public override void DoAbility(Action action)
        {
            foreach (var character in action.TargetParty.Characters)
            {
                // (15 to 20) + (1 to Wisdom)
                int healing = action.Actor.Random.Next(15, 25) + (int)(action.Actor.Stats.Wisdom * action.Actor.Random.NextDouble());
                character.DoHealing(action, healing);
            }
        }
    }
}
