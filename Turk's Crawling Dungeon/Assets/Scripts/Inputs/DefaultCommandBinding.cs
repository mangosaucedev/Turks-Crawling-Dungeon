using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Inputs
{
    public struct DefaultCommandBinding
    {
        public KeyCommand command;
        public Keybinding keybinding;

        public DefaultCommandBinding(KeyCommand command, Keybinding keybinding)
        {
            this.command = command;
            this.keybinding = keybinding;
        }
    }
}
