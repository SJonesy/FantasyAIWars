using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Console = Colorful.Console;

namespace FantasyAIWars.Abilities
{
    class GustOfWind : SpellAbility
    {
        public override string Name { get; }
        public override int Delay { get; }
        public override int Cooldown { get; }
        public override AbilityType Type { get; }
        public override string OutputText { get; }
        public override Color OutputColor { get; }
        public override DamageType DamageType { get; }

        public GustOfWind()
        {
            Name = "GustOfWind";
            Delay = 3;
            Cooldown = 3;
            ManaCost = 9;
            Type = AbilityType.AoE;
            DamageType = DamageType.Air;
            OutputText = "As {actor} finishes casting, {target} is slammed by a gust of wind for {damage}hp.";
            OutputColor = Color.PaleGreen;
        }

        public override void DoAbility(Action action)
        {
            // (15 to 20) + (1 to Intelligence)
            double totalDamage = action.Actor.Random.Next(22, 26) + (action.Actor.Stats.Intelligence * action.Actor.Random.NextDouble());
            List<Character> aliveEnemies = action.TargetParty.Characters.FindAll(c => c.IsAlive);
            int individualDamage = (int)(totalDamage / aliveEnemies.Count);

            foreach (var character in aliveEnemies)
            {
                character.DoDamage(action, individualDamage);
            }
        }
    }
}
