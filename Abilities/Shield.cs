using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyAIWars.Abilities
{
    class Shield : Ability
    {
        public override string Name { get; }
        public override int Delay { get; }
        public override int Cooldown { get; }
        public override AbilityType Type { get; }

        public Shield()
        {
            this.Name = "Shield";
            this.Type = AbilityType.Passive;
        }
    }
}
