namespace FantasyAIWars.Races
{
    class Skeleton : Race
    {
        public override string Name { get; }
        public override StatBlock Stats { get; }
        public override ResistBlock Resists { get; }

        public Skeleton()
        {
            Name = "Skeleton";
            Stats = new StatBlock(); // TODO: Can I abstract this out?
            Stats.Strength = 10;
            Stats.Dexterity = 10;
            Stats.Intelligence = 6;
            Stats.Wisdom = 8;
            Stats.Charisma = 6;
            Stats.Constitution = 10;

            Resists = new ResistBlock();
            Resists.Fire = .5f;
            Resists.Poison = 10.0f;
            Resists.Physical = 5.0f;
            Resists.Unholy = 2.0f;
            Resists.Holy = 0.25f;
            Resists.Air = 1.5f;
        }

        public override void Apply(Character character)
        {
            character.Stats = this.Stats;
            character.Resists = this.Resists;
            // TODO: lots of immunities
        }
    }
}
