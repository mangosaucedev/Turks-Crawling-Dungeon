using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Attacks;

namespace TCD.Objects.Parts
{
    public class Kill : Goal
    {
        private BaseObject target;

        public Kill(Brain brain, BaseObject target) : base(brain)
        {
            this.target = target;
        }

        public override int GetTimeCost() => 0;

        public override void PerformAction()
        {
            base.PerformAction();

            Think($"I'm trying to kill {target.display.GetDisplayName()}!");

            if (FailIfTargetDoesNotExist())
                return;

            if (TryToCloseIntoAttackDistance())
                return;

            if (TryToAttackTarget())
                return;
        }

        private bool FailIfTargetDoesNotExist()
        {
            if (!target)
            {
                Think("My target does not exist! Changing goal.");
                FailToParent();
                return true;
            }
            return false;
        }

        private bool TryToCloseIntoAttackDistance()
        {
            int distanceToTarget = 
                Query.distance.GetDistanceToInstanceInCells(target);
            int maxAttackRange = 
                Query.attack.GetMaxAttackRange();

            if (distanceToTarget > maxAttackRange)
            {
                Think("I am out of range to attack target; closing distance.");
                PushChildGoal(new MoveInRangeOfTarget(brain, target));
                return true;
            }
            else
            {
                Think("I am in attack range of target.");
                return false;
            }
        }

        private bool TryToAttackTarget()
        {
            if (!obj.parts.TryGet(out Combat combat))
            {
                Think("I am incapable of combat!");
                return false;
            }
            return AttackHandler.AutoAttack(obj, target);
        }

        public override bool IsFinished()
        {
            if (!target)
                return true;
            return false;
        }
    }
}