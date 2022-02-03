using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Cinematics.Dialogue
{
    public static class DialogueSpeakerFactory 
    {
        public static DialogueSpeaker Retrieve(string speakerName)
        {
            DialogueSpeaker speaker = Assets.Get<DialogueSpeaker>(speakerName);
            speaker.Initialize();
            return speaker;
        }
    }
}
