using System.Diagnostics;

namespace FantasyAIWars
{
    class Action
    {
        public Ability Ability;
        public Character Actor;
        public Character TargetCharacter;
        public Party TargetParty;

        public Action() { }
        public Action(Ability ability, Character actor, Character targetCharacter)
        {
            Debug.WriteLine("Action::Action({0}, {1}, {2})", ability, actor, targetCharacter);
            Ability = ability;
            Actor = actor;
            TargetCharacter = targetCharacter;
        }

        public Action(Ability ability, Character actor, Party targetParty)
        {
            Debug.WriteLine("Action::Action({0}, {1}, {2})", ability, actor, targetParty);
            Ability = ability;
            Actor = actor;
            TargetParty = targetParty;
        }

        public Action(Ability ability, Character actor)
        {
            Debug.WriteLine("Action::Action({0}, {1})", ability, actor);
            Ability = ability;
            Actor = actor;
        }

        public int GetDelay()
        {
            return Ability.GetDelay(this);
        }

        public override string ToString()
        {
            return string.Format("Ability: {0}, Actor: {1}, TargetCharacter: {2}, TargetParty: {3}", Ability, Actor.Name, TargetCharacter, TargetParty);
        }
    }
}
