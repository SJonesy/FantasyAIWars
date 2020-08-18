using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyAIWars
{
    class Character
    {
        public string Name { get; set; }
        public Race Race { get; set; }
        public List<Ability> Abilities { get; set; }
        public string Script { get; set; }

        public Character()
        {
        }

        public bool Init()
        {
            Console.WriteLine("{0}\t{1}\t[{2}]", Name, Race.ToString(), string.Join(", ", Abilities)); 
            return true;
        }
    }
}
