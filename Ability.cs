using FantasyAIWars.Abilities;
using System;
using System.Collections.Generic;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.ObjectFactories;

namespace FantasyAIWars
{
    abstract class Ability
    {
        public string Name;

        public override string ToString() 
        {
            return Name;
        }
    }

    public enum AbilityName
    {
        Idle,
        Melee,
        Shield
    }

}
