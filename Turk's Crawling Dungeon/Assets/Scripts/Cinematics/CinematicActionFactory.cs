using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Cinematics
{
    public static class CinematicActionFactory 
    {
        private const string INVALID_TYPE = "CinematicActionFactory could not create action for event of type {0}: invalid event type!";

        public static CinematicAction GetFromEvent(CinematicEvent e)
        {
            EventType type = e.GetEventType();
            switch (type)
            {
                case EventType.SetTarget:
                    return new SetTarget(e) { isRequired = e.isRequired };

                case EventType.WaitForSeconds:
                    return new WaitForXSeconds(e) { isRequired = e.isRequired };

                case EventType.CameraResetZoom:
                    return new CameraResetZoom(e) { isRequired = e.isRequired };

                case EventType.FadeColor:
                    return new FadeColor(e) { isRequired = e.isRequired };

                case EventType.StartDialogue:
                    return new StartDialogue(e) { isRequired = e.isRequired };

                case EventType.StartMonologue:
                    return new StartMonologue(e) { isRequired = e.isRequired };

                case EventType.ClearMonologue:
                    return new ClearMonologue(e) { isRequired = e.isRequired };

                case EventType.StartCinematic:
                    return new StartCinematic(e) { isRequired = e.isRequired };

                default:
                    throw new Exception(string.Format(INVALID_TYPE, type));
            }
        }
    }
}
