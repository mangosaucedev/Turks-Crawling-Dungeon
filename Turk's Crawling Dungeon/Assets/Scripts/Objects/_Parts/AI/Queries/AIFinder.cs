using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class AIFinder
    {
        private Brain brain;
        private float minDistance;
        private BaseObject target;

        public AIFinder(Brain brain)
        {
            this.brain = brain;
        }

        public BaseObject GetEnemyInSight()
        {
            Reset();
            Collider2D[] colliders = GetCollidersInSightRadius();
            foreach (Collider2D collider in colliders)
                EvaluateIfColliderParentIsValidTarget(collider);
            return target;
        }

        private Collider2D[] GetCollidersInSightRadius()
        {
            Vector2 position = brain.parent.cell.Position * Cell.SIZE;
            float radius = brain.SightRadius;
            int layerMask = LayerMask.GetMask("Objects");
            return Physics2D.OverlapCircleAll(position, radius, layerMask);
        }

        private void Reset()
        {
            minDistance = float.MaxValue;
            target = null;
        }

        private void EvaluateIfColliderParentIsValidTarget(Collider2D collider)
        {
            if (collider.TryGetComponentInParent(out BaseObject obj))
                SetTargetIfObjectIsValid(obj);
        }

        private void SetTargetIfObjectIsValid(BaseObject obj)
        {
            Vector2Int position = brain.Position;
            Vector2Int targetPosition = obj.cell.Position;
            float distance = Vector2Int.Distance(position, targetPosition);
            BaseObject parent = brain.parent;
            Brain otherBrain = obj.Parts.Get<Brain>();
            if (distance < minDistance && otherBrain && otherBrain.Faction != brain.Faction)
            {
                minDistance = distance;
                target = obj;
            }
        }
    }
}