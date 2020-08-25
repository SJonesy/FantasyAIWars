namespace FantasyAIWars
{
    abstract class Race
    {
        public abstract string Name { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
