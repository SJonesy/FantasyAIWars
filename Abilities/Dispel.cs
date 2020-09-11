using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FantasyAIWars.Abilities
{
    class Dispel : SpellAbility
    {
        public override string Name { get; }
        public override int Delay { get; }
        public override int Cooldown { get; }
        public override AbilityType Type { get; }
        public override string OutputText { get; }
        public override Color OutputColor { get; }
        public override DamageType DamageType { get; }

        public Dispel()
        {
            Name = "Dispel";
            Delay = 2;
            Cooldown = 4;
            ManaCost = 9;
            Type = AbilityType.Spell;
            DamageType = DamageType.None;
            OutputText = "As {actor} finishes casting, {target} feels a bit dispelled.";
            OutputColor = Color.AntiqueWhite;
        }

        public override void DoAbility(Action action)
        {
            if (action.TargetCharacter.PartyIndex == action.Actor.PartyIndex)
            {
                action.TargetCharacter.Debuffs.Clear();
            }
            else
            {
                action.TargetCharacter.Buffs.Clear();
            }

            DoOutput(action, 0, action.TargetCharacter);
        }
    }
}
