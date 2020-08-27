using System.Diagnostics;
using System.Drawing;
using Console = Colorful.Console;

namespace FantasyAIWars.Abilities
{
    abstract class MeleeAbility : Ability
    {
        public override void OnQueue(Action action)
        {
            if (action.TargetCharacter == action.Actor.EngagedWith)
                return;

            if (action.TargetCharacter.IsGuardedBy == null)
            {
                Console.WriteLine("{0} moves to engage {1}.", action.Actor.Name, action.TargetCharacter.Name, Color.Gray);
            }
            else
            {
                Console.WriteLine("{0} moves to engage {1}, but {2} intercepts him!",
                    action.Actor.Name, action.TargetCharacter.Name, action.TargetCharacter.IsGuardedBy.Name);
                action.TargetCharacter = action.TargetCharacter.IsGuardedBy;
            }

        }

        public override int GetDelay(Action action)
        {
            if (action.TargetCharacter == action.Actor.EngagedWith)
            {
                Debug.WriteLine("MeleeAbility.GetDelay: LastTarget(Party:{0}, Index: {1}) TargetCharacter(Party: {2}, Index: {3})",
                    action.Actor.EngagedWith.PartyIndex, action.Actor.EngagedWith.CharacterIndex, action.TargetCharacter.PartyIndex, action.TargetCharacter.CharacterIndex);
                return 2;
            }
            else
            {
                return Delay;
            }
        }
    }
}
