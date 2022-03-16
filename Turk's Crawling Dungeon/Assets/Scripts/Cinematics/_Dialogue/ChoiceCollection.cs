using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Cinematics.Dialogues
{
    [Serializable]
    public class ChoiceCollection 
    {
        public Choice[] choices;

        public int Count
        {
            get
            {
                if (choices == null)
                    return 0;
                return choices.Length;
            }
        }
    }
}
