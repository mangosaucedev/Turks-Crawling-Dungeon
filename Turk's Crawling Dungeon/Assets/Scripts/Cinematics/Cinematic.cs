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
        private int i;

        private List<CinematicAction> GetActions()
        {
            if (actions.Count == 0 && events != null && events.Length > 0)
            {
                foreach (CinematicEvent e in events)
                    actions.Add(CinematicActionFactory.GetFromEvent(e));
                actions.Reverse();
            }
            return actions;
        }

        public IEnumerator PlayRoutine()
        {
            for (i = GetActions().Count - 1; i >= 0; i--)
                yield return GetActions()[i].PerformRoutine();
        }

        public void Skip()
        {
            if (i < 0 || i >= GetActions().Count)
                return;
            int index = i;
            while (index >= 0 && !GetActions()[index].isRequired)
            {
                GetActions()[index].skip = true;
                index--;
            }
        }
    }
}
