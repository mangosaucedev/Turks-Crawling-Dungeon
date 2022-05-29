using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Texts;
using TCD.UI.Notifications;

namespace TCD.Objects.Parts
{
    public class TutorialCrowbar : Part
    {
        private bool beenPickedUp;

        public override string Name => "Tutorial Crowbar";

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == PickedUpEvent.id && !beenPickedUp)
                OnPickedUp();
            return base.HandleEvent(e);
        }

        private void OnPickedUp()
        {
            beenPickedUp = true;
            NotificationHandler.Notify(
                "Interaction",
                new GameText(
                    "Now that you know how to interact with objects, try interacting with the barred door to the east while you " +
                    "have the <c=Au>crowbar</> equipped to your inventory. Find the appropriate interaction and proceed to the " +
                    "next room."));
        }
    }
}
