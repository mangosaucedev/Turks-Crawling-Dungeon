using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class MoveInRangeOfTarget : MoveToTarget
    {
        public MoveInRangeOfTarget(Brain brain, BaseObject target) :
            base(brain, target)
        {

        }

        public override bool IsFinished()
        {
            if (Query.attack.IsInAttackRange(target))
                return true;
            return base.IsFinished();
        } 
    }
}