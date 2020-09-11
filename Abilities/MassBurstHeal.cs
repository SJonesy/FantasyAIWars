using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FantasyAIWars.Abilities
{
    class MassBurstHeal : SpellAbility
    {
        public override string Name { get; }
        public override int Delay { get; }
        public override int Cooldown { get; }
        public override AbilityType Type { get; }
        public override string OutputText { get; }
        public override Color OutputColor { get; }

        public MassBurstHeal()
        {
            Name = "MassBurstHeal";
            Delay = 2;
            Cooldown = 12;
            ManaCost = 40;
            Type = AbilityType.Heal;
            OutputText = "A burst of heavenly light engulfs {target}, as {actor}'s mass burst heal heals them for {damage}hp.";
            OutputColor = Color.Yellow;
        }

        public override void DoAbility(Action action)
        {
            foreach (var character in action.TargetParty.Characters)
            {
                // (20 to 30) + (1 to Wisdom)
                int healing = action.Actor.Random.Next(15, 25) + (int)(action.Actor.Stats.Wisdom * action.Actor.Random.NextDouble());
                character.DoHealing(action, healing);
            }
        }
    }
}
