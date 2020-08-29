using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Console = Colorful.Console;

namespace FantasyAIWars
{
    abstract class Ability
    {
        public abstract string Name { get; }

        /* Delay and Cooldown should probably range from 1-10, 
         * that gives me a significant range to tweak. */
        public abstract int Delay { get; }
        public abstract int Cooldown { get; }
        public abstract AbilityType Type { get; }
        public virtual DamageType DamageType { get; }
        public virtual string OutputText { get; }
        public virtual Color OutputColor { get; }

        public int ManaCost = 0;

        public abstract void DoAbility(Action action);

        public virtual void DoOutput(Action action, int healthChange, Character target)
        {
            string output = this.OutputText;
            healthChange = Math.Abs(healthChange);
            output = output.Replace("{damage}", healthChange.ToString());
            output = output.Replace("{healing}", healthChange.ToString());
            output = output.Replace("{heal}", healthChange.ToString());
            output = output.Replace("{health}", healthChange.ToString());
            output = output.Replace("{actor}", action.Actor.Name);
            output = output.Replace("{target}", target.Name);

            Color outputColor = OutputColor != null ? OutputColor : Color.Gray;
            Console.WriteLine(output, outputColor);
        }

        public virtual void DoPostAbility(Action action)
        {
            Character actor = action.Actor;

            List<Character> targets = new List<Character>();
            if (action.TargetCharacter != null)
                targets.Add(action.TargetCharacter);
            if (action.TargetParty != null)
                targets.AddRange(action.TargetParty.Characters);

            foreach (var target in targets)
            {
                if (target.HitPoints <= 0)
                {
                    target.IsAlive = false;
                    target.IsCasting = false;
                    target.IsUsingAbility = false;
                    target.AbilityInUse = null;
                    Console.WriteLine("*** {0} has died ***", target.Name, Color.Red);
                    return;
                }
                if (this.Type == AbilityType.Melee && target.IsAlive)
                {
                    actor.EngagedWith = target;
                    target.EngagedWith = actor;
                }
                if (target.IsCasting)
                {
                    /* At 75 health = 25% chance
                     * At 50 health = 50% chance
                     * At 25 health = 75% chance
                     * At 100+ health = 0% chance */
                    float interruptRollChance = Math.Max(0, (100 - target.HitPoints) / 100);
                    if (target.Random.NextDouble() < interruptRollChance && target.Random.Next(1, 20) > target.Stats.Wisdom)
                    {
                        Console.WriteLine("{0}'s casting of {1} was interrupted by {2}!", target.Name, target.AbilityInUse.Name, actor.Name);
                        target.Interrupted = true;
                    }
                }
            }
        }

        public virtual int GetDelay(Action action)
        {
            return Delay;
        }

        public virtual void OnQueue(Action action) { }

        public override string ToString()
        {
            return Name;
        }
    }
}
