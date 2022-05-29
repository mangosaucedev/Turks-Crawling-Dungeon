using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Cinematics;

namespace TCD.Objects.Parts
{
    public class TutorialDoor0 : Part
    {
        private bool hasOpened;

        public override string Name => "Tutorial Door 0";

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == DoorOpenedEvent.id && !hasOpened)
            {   
                hasOpened = true;
                ServiceLocator.Get<CinematicManager>().FireEvent("tutorialDoorOpened");
            }
            return base.HandleEvent(e);
        }
    }
}
