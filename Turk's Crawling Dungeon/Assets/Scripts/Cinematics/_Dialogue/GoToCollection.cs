using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Cinematics.Dialogues
{
    [Serializable]
    public class GoToCollection
    {
        public GoTo[] transitions;

        public GoTo GetTransition()
        {
            if (transitions == null)
                return null;
            foreach (GoTo goTo in transitions)
            {
                if (goTo.IsTraversable())
                    return goTo;
            }    
            return null;
        }
    }
}
