using System.Runtime.InteropServices.ComTypes;

namespace FantasyAIWars
{
    abstract class Race
    {
        public abstract string Name { get; }
        public abstract StatBlock Stats { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
