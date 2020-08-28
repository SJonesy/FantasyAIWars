using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Console = Colorful.Console;

namespace FantasyAIWars.Abilities
{
    class Heal : SpellAbility
    {
        public override string Name { get; }
        public override int Delay { get; }
        public override int Cooldown { get; }
        public override AbilityType Type { get; }

        public Heal()
        {
            Name = "Heal";
            Delay = 3;
            Cooldown = 3;
            ManaCost = 3;
            Type = AbilityType.Heal;
        }

        public override void DoAbility(Action action)
        {
            // (15 to 25) + (1 to Wisdom)
            int healing = action.Actor.Random.Next(15, 25) + (int)(action.Actor.Stats.Wisdom * action.Actor.Random.NextDouble());
            string output = String.Format("{0}'s body is covered in a healing light as {1} finishes casting Heal for {2} hp.", 
                action.TargetCharacter.Name, action.Actor.Name, healing);
            DoHealing(action.Actor, action.TargetCharacter, healing);
            Console.WriteLine(output, Color.LightGoldenrodYellow);
        }
    }
}
