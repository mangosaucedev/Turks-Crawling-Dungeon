using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Attacks;

namespace TCD.Objects.Parts
{
    public class Kill : Goal
    {
        private BaseObject target;

        private Combat Combat => brain.parent.parts.Get<Combat>();

        public Kill(Brain brain, BaseObject target) : base(brain)
        {
            this.target = target;
        }

        public override int GetTimeCost()
        {
            if (IsInAttackRange())
                return Combat?.GetAttackCost(target) ?? 0;
            return 0;
        }

        private bool IsInAttackRange()
        {
            int distanceToTarget =
                Query.distance.GetDistanceToInstanceInCells(target);
            int maxAttackRange =
                Query.attack.GetMaxAttackRange();
            return distanceToTarget <= maxAttackRange;
        }

        public override bool PerformAction()
        {
            if (!base.PerformAction())
                return false;

            Think($"I'm trying to kill {target.display.GetDisplayName()}!");

            if (FailIfTargetDoesNotExist())
                return false;

            if (TryToCloseIntoAttackDistance())
                return true;

            if (TryToAttackTarget())
                return true;

            return false;
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
            bool isIsInAttackRange = IsInAttackRange();
            if (!isIsInAttackRange)
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
            if (!Combat)
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