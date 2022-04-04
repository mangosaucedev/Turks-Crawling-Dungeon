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
                    return new SetTarget(e);
                case EventType.WaitForSeconds:
                    return new WaitForXSeconds(e);
                case EventType.FadeColor:
                    return new FadeColor(e);
                case EventType.StartMonologue:
                    return new StartMonologue(e);
                case EventType.ClearMonologue:
                    return new ClearMonologue(e);
                default:
                    throw new Exception(string.Format(INVALID_TYPE, type));
            }
        }
    }
}
