using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TCD.UI;

namespace TCD.Cinematics
{
    public class FadeColor : CinematicAction
    {
        private const int MIN_ARGUMENTS = 3;

        private Color color;
        private float alpha;
        private float fadeTime;

        public FadeColor(CinematicEvent e) : base(e)
        {

        }

        public override void ParseArguments()
        {
            if (arguments == null || arguments.Length < MIN_ARGUMENTS)
                return;
            color = Assets.Get<Color>(arguments[0]);
            CultureInfo info = CultureInfo.InvariantCulture;
            alpha = float.Parse(arguments[1], info);
            fadeTime = float.Parse(arguments[2], info);
        }

        public override IEnumerator PerformRoutine()
        {
            if (!ViewManager.IsOpen("Cinematic Fade"))
                ViewManager.Open("Cinematic Fade");
            CinematicFadeColor fade = ServiceLocator.Get<CinematicFadeColor>();
            yield return fade.FadeToColorRoutine(color, alpha, fadeTime);
        }
    }
}
