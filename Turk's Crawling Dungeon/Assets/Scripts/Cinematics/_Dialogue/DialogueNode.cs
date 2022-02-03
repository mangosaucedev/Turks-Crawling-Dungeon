using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Texts;

namespace TCD.Cinematics.Dialogue
{
    public class DialogueNode
    {
        public string name;
        public bool isOneShot;
        public string textName;
        public GameText text;
        public string speakerName;
        public DialogueSpeaker speaker;
        public string goToNodeName;
        public List<DialogueResponse> responses = new List<DialogueResponse>();

        public DialogueNode GoToNode
        {
            get
            {
                if (!HasGoToNode)
                    return null;
                return DialogueNodeFactory.Retrieve(goToNodeName);
            }
        }

        public bool HasGoToNode => !string.IsNullOrEmpty(goToNodeName);

        public void Initialize()
        {
            if (text == null)
                text = Assets.Get<GameText>(textName);
            if (speaker == null)
                speaker = DialogueSpeakerFactory.Retrieve(speakerName);
        }
    }
}
