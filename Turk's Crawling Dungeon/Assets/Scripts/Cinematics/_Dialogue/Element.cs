using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Cinematics.Dialogues
{
    [Serializable]
    public abstract class Element 
    {
        public string text;
        public GoToCollection transitions;

        public Dialogue GetTransition() => Assets.Get<Dialogue>(transitions.GetTransition().node);
        
    }
}
