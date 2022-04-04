using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Texts;
using TCD.UI;

namespace TCD.Cinematics
{
    public class StartMonologue : CinematicAction
    {
        private string text;
        private bool waitForPrint;

        public StartMonologue(CinematicEvent e) : base(e)
        {

        }

        public override void ParseArguments()
        {
            if (arguments == null || arguments.Length == 0)
                return;
            text = arguments[0];
            if (arguments.Length > 1)
                waitForPrint = bool.Parse(arguments[1]);
        }

        public override IEnumerator PerformRoutine()
        {
            GameText gameText = new GameText(text);
            if (!ViewManager.IsOpen("Cinematic Monologue"))
                ViewManager.Open("Cinematic Monologue");
            CinematicMonologue monologue = ServiceLocator.Get<CinematicMonologue>();
            if (waitForPrint)
                yield return monologue.StartMonologueRoutine(gameText);
            else
                monologue.StartMonologue(gameText);
        }
    }
}
