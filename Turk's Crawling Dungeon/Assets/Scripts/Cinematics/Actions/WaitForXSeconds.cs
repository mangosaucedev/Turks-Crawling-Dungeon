using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Cinematics
{
    public class WaitForXSeconds : CinematicAction
    {
        private float seconds;
        private WaitForSeconds wait;

        public WaitForXSeconds(CinematicEvent e) : base(e)
        {
            wait = new WaitForSeconds(seconds);    
        }

        public override void ParseArguments()
        {
            if (arguments == null || arguments.Length == 0)
                return;
            seconds = float.Parse(arguments[0]);
        }

        public override IEnumerator PerformRoutine()
        {
            yield return wait;
        }
    }
}
