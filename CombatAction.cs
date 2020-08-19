using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyAIWars
{
    class CombatAction
    {
        public Ability Ability;
        public Character Actor;
        public Character TargetCharacter;
        public Party TargetParty;
        public int tick;

        public CombatAction() { }
        public CombatAction(Ability ability, Character actor, Character targetCharacter)
        {
            Ability = ability;
            Actor = actor;
            TargetCharacter = targetCharacter;
        }

        public CombatAction(Ability ability, Character actor, Party targetParty)
        {
            Ability = ability;
            Actor = actor;
            TargetParty = targetParty;
        }

        public CombatAction(Ability ability, Character actor)
        {
            Ability = ability;
            Actor = actor;
        }
    }
}
