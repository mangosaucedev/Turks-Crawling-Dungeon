using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Cinematics.Dialogues
{
    [Serializable]
    public class Dialogue : Element 
    {
        public string name;
        public string speakerName;
        public bool oneShot;
        public string trigger;
        public ChoiceCollection choices;

        public Speaker Speaker => Assets.Get<Speaker>(speakerName);
    }
}
