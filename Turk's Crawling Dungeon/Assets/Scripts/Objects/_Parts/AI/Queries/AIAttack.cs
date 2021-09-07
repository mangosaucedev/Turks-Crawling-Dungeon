using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class AIAttack
    {
        private Brain brain;

        public AIAttack(Brain brain)
        {
            this.brain = brain;
        }

        public int GetMaxAttackRange()
        {
            return 1;
        }

        public int GetMinAttackRange()
        {
            return 0;
        }

        public bool IsInAttackRange(BaseObject target)
        {
            int distance = brain.query.distance.GetDistanceToInstanceInCells(target);
            return distance <= GetMaxAttackRange();
        }
    }
}