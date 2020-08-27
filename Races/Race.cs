namespace FantasyAIWars
{
    abstract class Race
    {
        public abstract string Name { get; }
        public abstract StatBlock Stats { get; }
        public abstract ResistBlock Resists { get; }

        public virtual void Apply(Character character)
        {
            character.Stats = this.Stats;
            character.Resists = this.Resists;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
