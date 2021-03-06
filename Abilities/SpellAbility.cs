﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Console = Colorful.Console;

namespace FantasyAIWars.Abilities
{
    abstract class SpellAbility : Ability
    {
        public override void OnQueue(Action action)
        {
            Console.WriteLine("{0} begins to cast a spell.", action.Actor.Name, Color.DarkGray);
            action.Actor.IsCasting = true;
            action.Actor.Mana -= action.Ability.ManaCost;
        }
    }
}
