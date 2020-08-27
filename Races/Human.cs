using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyAIWars.Races
{
    class Human : Race
    {
        public override string Name { get; }
        public override StatBlock Stats { get; }
        public override ResistBlock Resists { get; }

        public Human()
        {
            Name = "Human";
            Stats = new StatBlock(); // TODO: Can I abstract this out?
            Stats.Strength = 12;
            Stats.Dexterity = 12;
            Stats.Intelligence = 12;
            Stats.Wisdom = 12;
            Stats.Charisma = 12;
            Stats.Constitution = 12;

            Resists = new ResistBlock();
        }
    }
}
