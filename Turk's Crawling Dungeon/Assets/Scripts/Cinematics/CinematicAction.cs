using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Cinematics
{
    public abstract class CinematicAction
    {
        protected string[] arguments;

        public CinematicAction(CinematicEvent e)
        {
            arguments = e.arguments;
            ParseArguments();
        }

        public abstract void ParseArguments();

        public abstract IEnumerator PerformRoutine();
    }
}
