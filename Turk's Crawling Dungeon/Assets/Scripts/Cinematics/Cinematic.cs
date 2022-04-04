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
        public CinematicEvent[] events;

        private List<CinematicAction> actions = new List<CinematicAction>();

        private List<CinematicAction> GetActions()
        {
            if (actions.Count == 0 && events != null && events.Length > 0)
            {
                foreach (CinematicEvent e in events)
                    actions.Add(CinematicActionFactory.GetFromEvent(e));
            }
            return actions;
        }

        public IEnumerator PlayRoutine()
        {
            foreach (CinematicAction action in GetActions())
                yield return action.PerformRoutine();
        }
    }
}
