using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TCD.UI;

namespace TCD.Cinematics
{
    public class ClearMonologue : CinematicAction
    {
        private float fadeTime;
        private bool wait;

        public ClearMonologue(CinematicEvent e) : base(e)
        {
             
        }

        public override void ParseArguments()
        {
            if (arguments == null || arguments.Length == 0)
                return;
            CultureInfo info = CultureInfo.InvariantCulture;
            fadeTime = float.Parse(arguments[0], info);
            if (arguments.Length > 1)
                wait = bool.Parse(arguments[1]);
        }

        public override IEnumerator PerformRoutine()
        {
            if (!ViewManager.IsOpen("Cinematic Monologue"))
                yield break;
            CinematicMonologue monologue = ServiceLocator.Get<CinematicMonologue>();
            if (wait)
                yield return monologue.ClearMonologueRoutine(fadeTime);
            else
                monologue.ClearMonologue(fadeTime);
        }
    }
}
