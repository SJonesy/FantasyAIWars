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
            Ability = ability;
            Actor = actor;
            TargetCharacter = targetCharacter;
        }

        public Action(Ability ability, Character actor, Party targetParty)
        {
            Ability = ability;
            Actor = actor;
            TargetParty = targetParty;
        }

        public Action(Ability ability, Character actor)
        {
            Ability = ability;
            Actor = actor;
        }

        public int GetDelay()
        {
            return Ability.Delay;
        }
    }
}
