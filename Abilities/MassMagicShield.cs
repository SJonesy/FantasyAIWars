using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FantasyAIWars.Abilities
{
    class MassMagicShield : SpellAbility
    {
        public override string Name { get; }
        public override int Delay { get; }
        public override int Cooldown { get; }
        public override AbilityType Type { get; }
        public override string OutputText { get; }
        public override Color OutputColor { get; }
        public override DamageType DamageType { get; }

        public MassMagicShield()
        {
            Name = "MassMagicShield";
            Delay = 8;
            Cooldown = 8;
            ManaCost = 30;
            Type = AbilityType.GroupBuff;
            OutputText = "As {actor} finishes casting, a translucent shield appears around {target}.";
            OutputColor = Color.CornflowerBlue;
        }

        public override void DoAbility(Action action)
        {
            List<Character> aliveAllies = action.TargetParty.Characters.FindAll(c => c.IsAlive);

            foreach (var character in aliveAllies)
            {
                character.AddBuff(StatusEffect.MagicShield);
                DoOutput(action, 0, character);
            }
        }
    }
}
