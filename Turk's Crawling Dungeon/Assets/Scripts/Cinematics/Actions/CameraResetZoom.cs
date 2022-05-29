using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Texts;
using TCD.UI;

namespace TCD.Cinematics
{
    public class CameraResetZoom : CinematicAction
    {
        public CameraResetZoom(CinematicEvent e) : base(e)
        {

        }

        public override void ParseArguments()
        {

        }

        public override IEnumerator PerformRoutine()
        {
            ServiceLocator.Get<MainCamera>().ResetZoom();   
            yield return null;
        }
    }
}
