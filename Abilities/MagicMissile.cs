using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FantasyAIWars.Abilities
{
    class MagicMissile : SpellAbility
    {
        public override string Name { get; }
        public override int Delay { get; }
        public override int Cooldown { get; }
        public override AbilityType Type { get; }
        public override string OutputText { get; }
        public override Color OutputColor { get; }
        public override DamageType DamageType { get; }

        public MagicMissile()
        {
            Name = "MagicMissile";
            Delay = 3;
            Cooldown = 3;
            ManaCost = 6;
            Type = AbilityType.AttackSpell;
            DamageType = DamageType.Arcane;
            OutputText = "As {actor} finishes casting, a purple bolt of energy shoots out from their palm, striking {target} for {damage}hp.";
            OutputColor = Color.BlueViolet;
        }

        public override void DoAbility(Action action)
        {
            List<Character> aliveEnemies = action.TargetParty.Characters.FindAll(c => c.IsAlive);

            for (int i = 0; i < 3; i++)
            {
                int randomTarget = action.Actor.Random.Next(aliveEnemies.Count);
                // 5 + 1 to character int
                double damage = 2 + (action.Actor.Stats.Intelligence * action.Actor.Random.NextDouble());
                aliveEnemies[randomTarget].DoDamage(action, (int)damage);
            }
        }
    }
}
