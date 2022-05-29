using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.UI;

namespace TCD.Cinematics.Dialogues
{
    public static class DialogueHandler 
    {
        public static bool isInDialogue;

        private static Dialogue currentDialogue;
        private static HashSet<Dialogue> oneShotDialogues = new HashSet<Dialogue>();

        public static Dialogue CurrentDialogue => currentDialogue;

        public static bool GoToDialogueNode(string name)
        {
            Dialogue dialogue = Assets.Get<Dialogue>(name);
            return GoToDialogueNode(dialogue);
        }

        public static bool GoToDialogueNode(Dialogue dialogue)
        {
            if (dialogue == null)
            {
                EndDialogue();
                return true;
            }

            if (oneShotDialogues.Contains(dialogue))
                return false;

            DebugLogger.Log("[Dialogue] - Going to dialogue node " + dialogue);

            if (dialogue.oneShot)
                oneShotDialogues.Add(dialogue);

            currentDialogue = dialogue;
            if (!isInDialogue)
                StartDialogue();

            DialogueView dialogueView = ServiceLocator.Get<DialogueView>();
            dialogueView.DisplayDialogue(currentDialogue);

            if (!string.IsNullOrEmpty(dialogue.trigger))
                ServiceLocator.Get<CinematicManager>().FireEvent(dialogue.trigger);

            return true;
        }

        public static void EndDialogue()
        {
            DebugLogger.Log("[Dialogue] - Ending dialogue.");
            isInDialogue = false;
            currentDialogue = null;
            ViewManager.Close("Dialogue View");
        }

        private static void StartDialogue()
        {
            isInDialogue = true;
            ViewManager.Open("Dialogue View");
        }
    }
}
