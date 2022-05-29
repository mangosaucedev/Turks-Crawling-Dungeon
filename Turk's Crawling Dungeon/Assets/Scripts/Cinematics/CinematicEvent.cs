using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Cinematics
{
    [Serializable]
    public class CinematicEvent 
    {
        public string type;
        public string[] arguments;
        public bool isRequired;

        public EventType GetEventType()
        {
            switch (type.ToLower())
            {
                case "settarget":
                case "target":
                    return EventType.SetTarget;
                case "resettarget":
                    return EventType.ResetTarget;
                case "wait":
                case "waitseconds":
                case "waitfor":
                case "waitforseconds":
                    return EventType.WaitForSeconds;
                case "waitinput":
                case "waitforinput":
                    return EventType.WaitForInput;
                case "waitviews":
                case "waitforviews":
                    return EventType.WaitForViews;
                case "resetzoom":
                    return EventType.CameraResetZoom;
                case "fade":
                case "fadecolor":
                case "fadetocolor":
                    return EventType.FadeColor;
                case "dialogue":
                case "startdialogue":
                    return EventType.StartDialogue;
                case "monologue":
                case "startmonologue":
                    return EventType.StartMonologue;
                case "clearmonologue":
                    return EventType.ClearMonologue;
                case "startcinematic":
                    return EventType.StartCinematic;
                default:
                    return EventType.Unknown;            
            }
        }
    }
}
