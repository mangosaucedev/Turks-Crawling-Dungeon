using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Juice
{
    public static class MovementJuiceHandler
    {
        private static JuiceManager JuiceManager => ServiceLocator.Get<JuiceManager>();

        public static void Move(BaseObject mover, Vector2Int direction)
        {
            MoveToCell moveToCell = new MoveToCell(mover, direction);
            JuiceManager.AddAnimation(moveToCell);
        }
    }
}
