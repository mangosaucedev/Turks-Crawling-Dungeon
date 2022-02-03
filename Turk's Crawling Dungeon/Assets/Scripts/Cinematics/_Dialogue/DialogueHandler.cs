using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.UI;

namespace TCD.Cinematics.Dialogue
{
    [ContainsGameStatics]
    public static class DialogueHandler 
    {
        public static bool inDialogue;
        public static DialogueNode currentNode;
        [GameStatic(null)] public static List<DialogueNode> oneShotDialoguesPlayed;

        public static bool GoToDialogueNode(string nodeName)
        {
            if (oneShotDialoguesPlayed == null)
                oneShotDialoguesPlayed = new List<DialogueNode>();

            if (string.IsNullOrEmpty(nodeName))
            {
                EndDialogue();
                return false;
            }

            DialogueNode node = DialogueNodeFactory.Retrieve(nodeName);

            if (oneShotDialoguesPlayed.Contains(node))
                return false;
            else if (node.isOneShot)
                oneShotDialoguesPlayed.Add(node);

            if (!inDialogue)
                BeginDialogue();

            currentNode = node;      
            DialogueView view = ServiceLocator.Get<DialogueView>();
            view.DisplayDialogue(currentNode);
            return true;
        }
        
        private static void BeginDialogue()
        {
            inDialogue = true;
            ViewManager.Open("Dialogue View");
        }

        public static void EndDialogue()
        {
            inDialogue = false;
            ViewManager.Close("Dialogue View");
        }
    }
}
