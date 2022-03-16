using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Cinematics
{
    [Serializable]
    public class Cinematic 
    {
        public string name;
        public CinematicEventCollection events;

        private List<CinematicAction> actions = new List<CinematicAction>();

        public List<CinematicAction> GetActions()
        {
            if (actions.Count == 0 && events.Count > 0)
            {
                foreach (CinematicEvent e in events.events)
                    actions.Add(CinematicActionFactory.GetFromEvent(e));
            }
            return actions;
        }
    }
}
