namespace FantasyAIWars.Abilities
{
    class Shield : Ability
    {
        public override string Name { get; }
        public override int Delay { get; }
        public override int Cooldown { get; }
        public override AbilityType Type { get; }
        public override DamageType DamageType { get; }

        public Shield()
        {
            this.Name = "Shield";
            this.Type = AbilityType.Passive;
        }

        public override void DoAbility(Action action)
        {
            action.Actor.Resists.Physical += .5f;
        }
    }
}
