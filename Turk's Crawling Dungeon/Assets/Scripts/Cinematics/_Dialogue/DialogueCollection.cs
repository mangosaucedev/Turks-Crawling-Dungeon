using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Cinematics.Dialogues
{
    [Serializable]
    public class DialogueCollection 
    {
        public Dialogue[] dialogue;

        public int Count
        {
            get
            {
                if (dialogue == null)
                    return 0;
                return dialogue.Length;
            }
        }
    }
}
