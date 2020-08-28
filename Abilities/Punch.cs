﻿using System;
using Console = Colorful.Console;

namespace FantasyAIWars.Abilities
{
    class Punch : MeleeAbility
    {
        public override string Name { get; }
        public override int Delay { get; }
        public override int Cooldown { get; }
        public override AbilityType Type { get; }
        public override DamageType DamageType { get; }
        public override int EngagedDelay { get; }

        public Punch()
        {
            Name = "Punch";
            Delay = 8;
            EngagedDelay = 2;
            Cooldown = 1;
            Type = AbilityType.Melee;
            DamageType = DamageType.Physical;
        }

        public override void DoAbility(Action action)
        {
            int damage = action.Actor.Random.Next(1, 5);
            damage += action.Actor.Stats.Strength - 12;
            int modifiedDamage = ModifyDamage(damage, action.TargetCharacter);
            DoDamage(action.Actor, action.TargetCharacter, modifiedDamage, true);

            string output = String.Format("{0} punches {1} right in the mouth for {2} damage.", action.Actor.Name, action.TargetCharacter.Name, modifiedDamage);
            Console.WriteLine(output);
        }
    }
}
