using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Attacks;
using TCD.Inputs.Actions;
using TCD.UI;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class Throwable : Part
    {
        [SerializeField] private int range;
        [SerializeField] private string attack;

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

        public void Throw(Vector2Int startPosition, Vector2Int targetPosition)
        {
            Item item = parent.Parts.Get<Item>();
            if (!item || !item.inventory || (item.inventory && item.inventory.TryRemoveItem(parent)))
            {
                GridRaycastResult result = GridRaycaster.Raycast(startPosition, targetPosition, new BlockedByObstaclesEvaluator());
                Vector2Int collisionPoint = targetPosition;
                if (result.collision)
                {
                    collisionPoint = result.ray.positions[result.collisionIndex];
                    if (result.collisionIndex != 0)
                        targetPosition = result.ray.positions[result.collisionIndex - 1];
                    else
                        targetPosition = startPosition;
                }
                parent.cell.SetPosition(targetPosition);
                AttackAtPosition(collisionPoint);
                OnThrown();
            }
        }

        private void AttackAtPosition(Vector2Int position)
        {
            Cell cell = CurrentZoneInfo.grid[position];
            for (int i = cell.objects.Count - 1; i >= 0; i--)
            {
                BaseObject obj = cell.objects[i];
                if (obj && parent != obj)
                    AttackHandler.AttackTarget(parent, obj, ThrowAttack);
            }
        }


        private void OnThrown()
        {

        }
    }
}
