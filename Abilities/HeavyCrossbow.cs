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

        public HeavyCrossbow()
        {
            Name = "HeavyCrossbow";
            Delay = 2;
            Cooldown = 10;
            Type = AbilityType.Attack;
            DamageType = DamageType.Physical;
        }

        public override void DoAbility(Action action)
        {
            int damage = action.Actor.Random.Next(25, 50);
            int modifiedDamage = ModifyDamage(damage, action.TargetCharacter);
            DoDamage(action.Actor, action.TargetCharacter, modifiedDamage, true);

            string output = String.Format("{0} shoots his heavy crossbow at {1} for {2}hp and begins to slowly reload.", action.Actor.Name, action.TargetCharacter.Name, modifiedDamage);
            Console.WriteLine(output, Color.PaleVioletRed);
        }
    }
}
