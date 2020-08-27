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
        public abstract DamageType DamageType { get; }

        public abstract void DoAbility(Action action);

        public void DoDamage(Character actor, Character target, int damage, bool alreadyModified = false)
        {
            if (!alreadyModified)
                damage = ModifyDamage(damage, target);

            target.HitPoints -= (int)damage;

            if (this.Type == AbilityType.Melee)
            {
                actor.EngagedWith = target;
                target.EngagedWith = actor;
            }

            if (target.HitPoints <= 0)
                target.IsAlive = false;
        }

        public int ModifyDamage(float damage, Character target)
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

            return (int)damage;
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
