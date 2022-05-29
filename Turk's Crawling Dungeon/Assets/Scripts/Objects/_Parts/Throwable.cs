using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Attacks;
using TCD.Inputs.Actions;
using TCD.Texts;
using TCD.TimeManagement;
using TCD.UI;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class Throwable : Part
    {
        [SerializeField] private int range;
        [SerializeField] private string attack;

        private Vector2Int lastThrowStartPosition;
        private Vector2Int lastThrowTargetPosition;

        public override string Name => "Throwable";

        public int Range
        {
            get => range;
            set => range = value;
        }

        public string Attack
        {
            get => attack;
            set => attack = value;
        }

        public Attack ThrowAttack => 
            AttackFactory.BuildFromBlueprint(Attack);

        protected override void GetInteractions(GetInteractionsEvent e)
        {
            base.GetInteractions(e);
            e.interactions.Add(new Interaction("Throw", OnPlayerThrow));
        }

        public void OnPlayerThrow()
        {
            ViewManager.Close("Player Inventory View");
            PlayerActionManager playerActionManager = ServiceLocator.Get<PlayerActionManager>();
            playerActionManager.TryStartAction(new Throw(parent), true);
        }

        public void Throw(BaseObject thrower, Vector2Int startPosition, Vector2Int targetPosition)
        {
            lastThrowStartPosition = startPosition + ObjectUtility.GetDirectionToVector(startPosition, targetPosition);
            lastThrowTargetPosition = targetPosition;
            ActionScheduler.EnqueueAction(thrower, Throw);
        }

        private void Throw()
        {
            Vector2Int startPosition = lastThrowStartPosition;
            Vector2Int targetPosition = lastThrowTargetPosition;
            Item item = parent.Parts.Get<Item>();
            if (!item || !item.inventory || (item.inventory && item.inventory.TryRemoveItem(parent)))
            {
                GridRaycastResult result = GridRaycaster.Raycast(startPosition, targetPosition, new BlockedByObstaclesEvaluator());
                if (result.collision && result.ray.positions.Count >= 2 && MarchForCollisionPosition(result.ray, out targetPosition, out Vector2Int collisionPoint))
                {
                    if (!AttackAtPosition(collisionPoint))
                        FloatingTextHandler.Draw(GameGrid.GridToWorld(collisionPoint), "Missed!", Color.red);
                }
                parent.cell.SetPosition(targetPosition);
                OnThrown();
            }
        }

        private bool MarchForCollisionPosition(GridRay ray, out Vector2Int targetPosition, out Vector2Int collisionPosition)
        {
            targetPosition = ray.positions[0];
            collisionPosition = ray.positions[1];
            Vector2Int lastPosition = ray.positions[0];
            for (int i = 1; i < ray.positions.Count; i++)
            {
                Vector2Int position = ray.positions[i];
                if (GameGrid.Current[position].Contains(out Obstacle obstacle) && obstacle.IsImpassable)
                {
                    collisionPosition = position;
                    targetPosition = lastPosition;
                    return true;
                }
                lastPosition = position;
            }
            return false;
        }

        private bool AttackAtPosition(Vector2Int position)
        {
            bool successful = false;
            Cell cell = CurrentZoneInfo.grid[position];
            for (int i = cell.Objects.Count - 1; i >= 0; i--)
            {
                BaseObject obj = cell.Objects[i];
                if (obj && parent != obj)
                    successful = successful || AttackHandler.AttackTarget(parent, obj, ThrowAttack);
            }
            return successful;
        }


        private void OnThrown()
        {

        }
    }
}
