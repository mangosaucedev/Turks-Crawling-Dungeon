using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Cinematics.Dialogue;
using TCD.UI;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class EyeTv : Part
    {
        public override string Name => "Eye Tv";

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == BeforeInspectEvent.id)
                return BeforeInspect();
            return base.HandleEvent(e);
        }

        private bool BeforeInspect()
        {
            if (!ViewManager.TryGetActiveView(out ActiveView activeView) || !activeView.locksInput)
            {
                if (DialogueHandler.GoToDialogueNode("EyeballTv0_0"))
                    return false;
                if (DialogueHandler.GoToDialogueNode("EyeballTv1_0"))
                    return false;
                if (DialogueHandler.GoToDialogueNode("EyeballTv2_0"))
                    return false;
            }
            return true;
        }
    }
}
