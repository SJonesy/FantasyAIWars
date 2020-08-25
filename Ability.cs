namespace FantasyAIWars
{
    abstract class Ability
    {
        public abstract string Name { get; }
        public abstract int Delay { get; }
        public abstract int Cooldown { get; }
        public abstract AbilityType Type { get; }

        public override string ToString() 
        {
            return Name;
        }
    }
}
