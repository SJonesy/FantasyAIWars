using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FantasyAIWars.Abilities
{
    class Melee : Ability
    {
        public override string Name { get; }
        public override int Delay { get; }
        public override int Cooldown { get; }
        public override AbilityType Type { get; }

        public Melee()
        {
            Name = "Melee";
            Delay = 1;
            Cooldown = 5;
            this.Type = AbilityType.Attack;
        }
    }
}
