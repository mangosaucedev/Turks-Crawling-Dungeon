using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
            CultureInfo info = CultureInfo.InvariantCulture;
            seconds = float.Parse(arguments[0], info);
        }

        public override IEnumerator PerformRoutine()
        {
            yield return wait;
        }
    }
}
