using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Cinematics
{
    [Serializable]
    public class CinematicEventCollection
    {
        public CinematicEvent[] events;

        public int Count
        {
            get
            {
                if (events == null)
                    return 0;
                return events.Length;
            }
        }
    }
}
