using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Texts;
using TCD.UI;

namespace TCD.Cinematics
{
    public class StartCinematic : CinematicAction
    {
        private string cinematic;

        public StartCinematic(CinematicEvent e) : base(e)
        {

        }

        public override void ParseArguments()
        {
            if (arguments == null || arguments.Length == 0)
                return;
            cinematic = arguments[0];
        }

        public override IEnumerator PerformRoutine()
        {
            ServiceLocator.Get<CinematicManager>().PlayCinematic(cinematic);
            yield return null;
        }
    }
}
