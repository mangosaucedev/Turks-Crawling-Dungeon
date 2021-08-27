using System;
using System.Collections;
using System.Collections.Generic;
using TCD.Objects;
using TCD.Texts;
using UnityEngine;

namespace TCD.Inputs.Actions
{
    public class PlayerLongAction : PlayerAction
    {
        public Func<bool> isComplete;
        public Func<bool> action;
        public Action onComplete;
        public bool enemiesInterrupt;

        public override string Name => "Long Action";

        public override MovementMode MovementMode => MovementMode.Free;

        public PlayerLongAction(Func<bool> action, Func<bool> isComplete, Action onComplete, bool enemiesInterrupt = true)
        {
            this.action = action;
            this.isComplete = isComplete;
            this.onComplete = onComplete;
            this.enemiesInterrupt = enemiesInterrupt;
        }

        public override void Start()
        {
            base.Start();
            PlayerActionManager playerActionManager = ServiceLocator.Get<PlayerActionManager>();
            playerActionManager.StartCoroutine(UpdateRoutine());
        }

        private IEnumerator UpdateRoutine()
        {
            while (Update())
                yield return null;
        }

        public bool Update()
        {
            if (enemiesInterrupt && IsEnemySpotted())
            {
                Cancel();
                FloatingTextHandler.Draw(PlayerInfo.currentPlayer.transform.position, "Enemy spotted!", Color.red);
                return false;
            }
            if (!action())
            {
                Cancel();
                return false;
            }
            if (isComplete())
            {
                Complete();
                return false;
            }
            return true;
        }

        private bool IsEnemySpotted()
        {
            foreach (Vector2Int position in FieldOfView.visiblePositions)
            {
                Cell cell = CurrentZoneInfo.grid[position];
                if (cell.objects.Find(o => o.faction == "Enemy"))
                    return true;
            }
            return false;
        }

        private void Cancel()
        {
            PlayerActionManager playerActionManager = ServiceLocator.Get<PlayerActionManager>();
            playerActionManager.CancelCurrentAction();
        }

        public void Complete()
        {
            onComplete();
            Cancel();
        }

        public override IEnumerator OnCell(Cell cell)
        {
            throw new NotImplementedException();
        }

        public override IEnumerator OnObject(BaseObject target)
        {
            throw new NotImplementedException();
        }

        public override int GetRange() => 1;
    }
}
