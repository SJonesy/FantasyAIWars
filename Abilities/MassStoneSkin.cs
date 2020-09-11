using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FantasyAIWars.Abilities
{
    class MassStoneSkin : SpellAbility
    {
        public override string Name { get; }
        public override int Delay { get; }
        public override int Cooldown { get; }
        public override AbilityType Type { get; }
        public override string OutputText { get; }
        public override Color OutputColor { get; }
        public override DamageType DamageType { get; }

        public MassStoneSkin()
        {
            Name = "MassStoneSkin";
            Delay = 8;
            Cooldown = 8;
            ManaCost = 30;
            Type = AbilityType.GroupBuff;
            OutputText = "As {actor} finishes casting, {target}'s skin turns hard as stone.";
            OutputColor = Color.AntiqueWhite;
        }

        public override void DoAbility(Action action)
        {
            List<Character> aliveAllies = action.TargetParty.Characters.FindAll(c => c.IsAlive);

            foreach (var character in aliveAllies)
            {
                character.AddBuff(StatusEffect.StoneSkin);
                DoOutput(action, 0, character);
            }
        }
    }
}
