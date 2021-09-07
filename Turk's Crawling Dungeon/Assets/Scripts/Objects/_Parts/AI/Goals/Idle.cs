using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class Idle : Goal
    {
        public Idle(Brain brain) : base(brain)
        {

        }

        public override int GetTimeCost() => 0;

        public override bool PerformAction()
        {
            if (!base.PerformAction())
                return false;

            Think("I'm idle.");

            if (TryToGetEnemyTarget())
                return true;

            return true;
        }

        private bool TryToGetEnemyTarget()
        {
            BaseObject enemyTarget =
                Query.finder.GetEnemyInSight();

            if (enemyTarget)
            {
                Think("I've sighted an enemy, moving to attack.");
                PushGoal(new Kill(brain, enemyTarget));
                return true;
            }

            Think("There are no enemies in my sight.");
            return false;
        }

        public override bool IsFinished() => true;
    }
}