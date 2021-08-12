using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Inputs
{
    public struct KeyEventContext
    {
        public static KeyEventContext none = new KeyEventContext();

        public KeyCommand command;
        public KeyState state;

        public KeyEventContext(KeyCommand command, KeyState state)
        {
            this.command = command;
            this.state = state;
        }
    }
}
