using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Cinematics.Dialogue
{
    public class DialogueSpeaker
    {
        public string name;
        public string displayName;
        public string colorName;
        public Color color;
        public string portraitName;
        public Sprite portrait;

        public void Initialize()
        {
            if (color == null)
                color = Assets.Get<Color>(colorName);
            if (portrait == null)
                portrait = Assets.Get<Sprite>(portraitName);
        }
    }
}
