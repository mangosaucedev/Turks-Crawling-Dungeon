using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Texts;

namespace TCD.Cinematics.Dialogue
{
    public class DialogueResponse 
    {
        public GameText text;
        public string goToNodeName;

        public bool HasGoToNode => !string.IsNullOrEmpty(goToNodeName);

        public DialogueNode GoToNode => DialogueNodeFactory.Retrieve(goToNodeName);
    }
}
