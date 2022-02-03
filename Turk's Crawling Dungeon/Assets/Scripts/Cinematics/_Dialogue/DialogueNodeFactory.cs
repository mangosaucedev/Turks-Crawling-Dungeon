using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Cinematics.Dialogue
{
    public static class DialogueNodeFactory
    {
        public static DialogueNode Retrieve(string nodeName)
        {
            DialogueNode node = Assets.Get<DialogueNode>(nodeName);
            node.Initialize();
            return node;
        }
    }
}
