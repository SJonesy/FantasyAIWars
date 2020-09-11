namespace FantasyAIWars.Abilities
{
    class Tough : Ability
    {
        public override string Name { get; }
        public override int Delay { get; }
        public override int Cooldown { get; }
        public override AbilityType Type { get; }
        public override DamageType DamageType { get; }

        public Tough()
        {
            this.Name = "Tough";
            this.Type = AbilityType.Passive;
        }

        public override void DoAbility(Action action)
        {
            action.Actor.Stats.Constitution += 2;

            action.Actor.Resists.Ice += .1f;
            action.Actor.Resists.Fire += .1f;
            action.Actor.Resists.Poison += .1f;
            action.Actor.Resists.Holy += .1f;
            action.Actor.Resists.Unholy += .1f;
            action.Actor.Resists.Water += .1f;
            action.Actor.Resists.Air += .1f;
            action.Actor.Resists.Earth += .1f;
            action.Actor.Resists.Physical += .1f;
            action.Actor.Resists.Arcane += .1f;
        }
    }
}
