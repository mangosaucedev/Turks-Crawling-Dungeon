using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class MoveToTarget : MoveTo
    {
        protected BaseObject target;

        public MoveToTarget(Brain brain, BaseObject target) : 
            base(brain, target.cell.Position)
        {
            this.target = target;
        }

        public override bool PerformAction()
        {
            if (target == null)
            {
                Think("My target no longer exists!");
                FailToParent();
                return false;
            }
           
            return base.PerformAction();
        }

        protected override Vector2Int GetTargetPosition() =>
            target?.cell.Position ?? brain.Position;
    }
}