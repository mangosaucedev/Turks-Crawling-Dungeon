using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Cinematics.Dialogues
{
    [Serializable]
    public class Speaker
    {
        public string name;
        public string displayName;
        public string portrait;
        public string color;

        public Sprite Sprite => Assets.Get<Sprite>(portrait);

        public Color Color => Assets.Get<Color>(color);
    }
}
