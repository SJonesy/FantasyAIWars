namespace FantasyAIWars.Races
{
    class Dwarf : Race
    {
        public override string Name { get; }
        public override StatBlock Stats { get; }
        public override ResistBlock Resists { get; }

        public Dwarf()
        {
            Name = "Dwarf";
            Stats = new StatBlock(); // TODO: Can I abstract this out?
            Stats.Strength = 14;
            Stats.Dexterity = 8;
            Stats.Intelligence = 10;
            Stats.Wisdom = 14;
            Stats.Charisma = 6;
            Stats.Constitution = 14;

            Resists = new ResistBlock();
            Resists.Ice = 1.1f;
            Resists.Fire = 1.1f;
            Resists.Poison = 1.1f;
            Resists.Earth = 1.3f;
            Resists.Physical = 1.2f;
        }
    }
}
