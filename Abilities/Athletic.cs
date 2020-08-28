using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyAIWars.Abilities
{
    class Athletic : Ability
    {
        public override string Name { get; }
        public override int Delay { get; }
        public override int Cooldown { get; }
        public override AbilityType Type { get; }
        public override DamageType DamageType { get; }

        public Athletic()
        {
            this.Name = "Athletic";
            this.Type = AbilityType.Passive;
        }

        public override void DoAbility(Action action)
        {
            action.Actor.Stats.Strength += 3;
            action.Actor.Stats.Dexterity += 3;
        }
    }
}
