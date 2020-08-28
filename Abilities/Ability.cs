using System;
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
        public int ManaCost = 0;

        public abstract void DoAbility(Action action);

        public virtual void DoHealing(Character actor, Character target, int healing, bool alreadyModified = false)
        {
            target.HitPoints = Math.Min(target.MaxHitPoints, target.HitPoints + healing);
        }

        public virtual void DoDamage(Character actor, Character target, int damage, bool alreadyModified = false)
        {
            if (!alreadyModified)
                damage = ModifyDamage(damage, target);

            target.HitPoints -= (int)damage;
        }

        public virtual void DoPostAbility(Action action)
        {
            Character actor = action.Actor;
            Character target = action.TargetCharacter;

            // TODO: should these post-damage checks be done somewhere else?
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
                float interruptRollChance = Math.Max(0, 100 - (target.HitPoints / 100));
                if (target.Random.NextDouble() < interruptRollChance && target.Random.Next(1, 20) > target.Stats.Wisdom)
                {
                    Console.WriteLine("{0}'s casting of {1} was interrupted by {2}!", target.Name, target.AbilityInUse.Name, actor.Name);
                    target.Interrupted = true;
                }
            }
        }

        public virtual int ModifyDamage(float damage, Character target)
        {
            if (this.DamageType == DamageType.Ice) damage /= target.Resists.Ice;
            else if (this.DamageType == DamageType.Fire) damage /= target.Resists.Fire;
            else if (this.DamageType == DamageType.Poison) damage /= target.Resists.Poison;
            else if (this.DamageType == DamageType.Holy) damage /= target.Resists.Holy;
            else if (this.DamageType == DamageType.Unholy) damage /= target.Resists.Unholy;
            else if (this.DamageType == DamageType.Water) damage /= target.Resists.Water;
            else if (this.DamageType == DamageType.Air) damage /= target.Resists.Air;
            else if (this.DamageType == DamageType.Earth) damage /= target.Resists.Earth;
            else if (this.DamageType == DamageType.Physical) damage /= target.Resists.Physical;

            return Math.Max((int)damage, 0);
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
