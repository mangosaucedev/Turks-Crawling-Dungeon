using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Indicators;
using TCD.Objects;
using TCD.Objects.Parts;
using TCD.Pathfinding;

namespace TCD.Inputs.Actions
{
    public class MoveToMouse : PlayerAction
    {
        public override string Name => "Move To Mouse";

        public override MovementMode MovementMode => MovementMode.Cursor;

        public override int GetRange() => 999;

        public override void Start()
        {
            base.Start();
            IndicatorHandler.ShowIndicator("Player Path To Cursor Indicator", 999, false);
        }

        public override void End()
        {
            base.End();
            IndicatorHandler.HideIndicator();
        }

        public override IEnumerator OnObject(BaseObject target)
        {
            BaseObject player = PlayerInfo.currentPlayer;
            if (!player.parts.TryGet(out Movement movement))
                yield break;
            Vector2Int startPosition = player.cell.Position;
            Vector2Int targetPosition = target.cell.Position;
            NavAstarPath path = new NavAstarPath(startPosition, targetPosition, new PlayerPathEvaluator());
            yield return movement.TryToNavigatePathRoutine(path);
            bool successful = movement.navigatedPath;
            DebugLogger.Log("Move to mouse was successful?" + successful.ToString());
        }


        public override IEnumerator OnCell(Cell cell)
        {
            BaseObject player = PlayerInfo.currentPlayer;
            if (!player.parts.TryGet(out Movement movement))
                yield break;
            Vector2Int startPosition = player.cell.Position;
            Vector2Int targetPosition = cell.Position;
            NavAstarPath path = new NavAstarPath(startPosition, targetPosition, new PlayerPathEvaluator());
            yield return movement.TryToNavigatePathRoutine(path);
            bool successful = movement.navigatedPath;
            DebugLogger.Log("Move to mouse was successful?" + successful.ToString());
        }
    }
}
