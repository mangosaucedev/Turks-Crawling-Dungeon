using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Cinematics.Dialogues;
using TCD.UI;

namespace TCD.Cinematics
{
    public class StartDialogue : CinematicAction
    {
        private string dialogue;

        public StartDialogue(CinematicEvent e) : base(e)
        {

        }

        public override void ParseArguments()
        {
            if (arguments == null || arguments.Length == 0)
                return;
            dialogue = arguments[0];
        }

        public override IEnumerator PerformRoutine()
        {
            DialogueHandler.GoToDialogueNode(dialogue);
            yield return new WaitUntil(() => { return !ViewManager.IsOpen("Dialogue View"); });
        }
    }
}
