using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Cinematics
{
    public class SetTarget : TargetAction
    {
        private const string PATTERN = "()";

        public SetTarget(CinematicEvent e) : base(e)
        {

        }

        public override void ParseArguments()
        {
            base.ParseArguments();
        }

        public override IEnumerator PerformRoutine()
        {
            yield return null;
        }
    }
}
