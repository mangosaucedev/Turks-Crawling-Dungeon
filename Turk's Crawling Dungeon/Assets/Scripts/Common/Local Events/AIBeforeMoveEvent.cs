using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class AIBeforeMoveEvent : AICommandEvent
    {
        public static new readonly string id = "AI Before Move";

        public Cell nextCell;
        public Vector2Int targetPosition;

        public override string Id => id;

        ~AIBeforeMoveEvent() => ReturnToPool();

        protected override void Reset()
        {
            base.Reset();
            nextCell = null;
            targetPosition = Vector2Int.zero;
        }
    } 
}
