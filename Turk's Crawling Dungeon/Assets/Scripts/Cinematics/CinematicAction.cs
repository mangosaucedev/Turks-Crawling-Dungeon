using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Cinematics
{
    public abstract class CinematicAction
    {
        private const string INVALID_ARGUMENTS = "Invalid arguments given for event type {0}.";

        public bool isRequired;
        public bool skip;

        protected string[] arguments;

        public CinematicAction(CinematicEvent e)
        {
            try
            {
                arguments = e.arguments;
                ParseArguments();
            }
            catch
            {
                ExceptionHandler.HandleMessage(string.Format(INVALID_ARGUMENTS, e.type));
            }
        }

        public abstract void ParseArguments();

        public abstract IEnumerator PerformRoutine();
    }
}
