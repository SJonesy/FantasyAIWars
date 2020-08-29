using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Console = Colorful.Console;

namespace FantasyAIWars.Abilities
{
    class HeavyCrossbow : Ability
    {
        public override string Name { get; }
        public override int Delay { get; }
        public override int Cooldown { get; }
        public override AbilityType Type { get; }
        public override DamageType DamageType { get; }
        public override string OutputText { get; }
        public override Color OutputColor { get; }

        public HeavyCrossbow()
        {
            Name = "HeavyCrossbow";
            Delay = 2;
            Cooldown = 10;
            Type = AbilityType.Attack;
            DamageType = DamageType.Physical;
            OutputText = "{actor} shoots his heavy crossbow at {target} for {damage}hp and begins to slowly reload.";
            OutputColor = Color.PaleVioletRed;
        }

        public override void DoAbility(Action action)
        {
            int damage = action.Actor.Random.Next(25, 50);
            action.TargetCharacter.DoDamage(action, damage);
        }
    }
}
