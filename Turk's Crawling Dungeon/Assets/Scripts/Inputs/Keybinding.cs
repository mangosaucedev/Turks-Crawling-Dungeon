using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Inputs
{
    public class Keybinding 
    {
        public KeyCode key = KeyCode.None;
        public KeyCode primaryModifier = KeyCode.None;
        public KeyCode secondaryModifier = KeyCode.None;

        public Keybinding()
        {

        }

        public Keybinding(
            KeyCode key, KeyCode primaryModifier, KeyCode secondaryModifier) : base()
        {
            this.key = key;
            this.primaryModifier = primaryModifier;
            this.secondaryModifier = secondaryModifier;
        }
    }
}
