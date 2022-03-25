using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD.Cinematics
{
    public abstract class TargetAction : CinematicAction
    {
        protected CinematicTarget target;

        public TargetAction(CinematicEvent e) : base(e)
        {

        }

        public override void ParseArguments()
        {
            target = GetTarget();
        }

        private CinematicTarget GetTarget()
        {
            return null;
        }
    }
}
