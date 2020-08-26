namespace FantasyAIWars
{
    abstract class Ability
    {
        public abstract string Name { get; }
        public abstract int Delay { get; }
        public abstract int Cooldown { get; }
        public abstract AbilityType Type { get; }
        public abstract DamageType DamageType { get; }

        public abstract void DoAbility(Action action);

        public void DoDamage(Character actor, Character target, float damage)
        {
            if      (this.DamageType == DamageType.Ice     ) damage = damage / actor.IceResist;
            else if (this.DamageType == DamageType.Fire    ) damage = damage / actor.FireResist;
            else if (this.DamageType == DamageType.Poison  ) damage = damage / actor.PoisonResist;
            else if (this.DamageType == DamageType.Holy    ) damage = damage / actor.HolyResist;
            else if (this.DamageType == DamageType.Unholy  ) damage = damage / actor.UnholyResist;
            else if (this.DamageType == DamageType.Water   ) damage = damage / actor.WaterResist;
            else if (this.DamageType == DamageType.Air     ) damage = damage / actor.AirResist;
            else if (this.DamageType == DamageType.Earth   ) damage = damage / actor.EarthResist;
            else if (this.DamageType == DamageType.Physical) damage = damage / actor.PhysicalResist;

            target.HitPoints -= (int)damage;

            if (target.HitPoints <= 0)
                target.IsAlive = false;
        }

        public override string ToString() 
        {
            return Name;
        }
    }
}
