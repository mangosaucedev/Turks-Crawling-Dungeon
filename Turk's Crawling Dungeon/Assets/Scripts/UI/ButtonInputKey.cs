using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.UI
{
    public class ButtonInputKey
    {
        public string str;
        public KeyCode key;
        public KeyCode primaryModifier;
        public KeyCode secondaryModifier;

        public ButtonInputKey(
            string str, KeyCode key, KeyCode primaryModifier = KeyCode.None, KeyCode secondaryModifier = KeyCode.None)
        {
            this.str = str;
            this.key = key;
            this.primaryModifier = primaryModifier;
            this.secondaryModifier = secondaryModifier;
        }
    }
}
