using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD
{
    public class GetMoveCostToCellEvent : ObjectEvent
    {
        public Cell cell;
        public BaseObject obj;
        public int cost;
        public List<BaseObject> obstacles = new List<BaseObject>();

        public static new readonly string id = "Get Move Cost To Cell";

        public override BaseObject Object => obj;

        public override string Id => id;

        ~GetMoveCostToCellEvent() => ReturnToPool();

        protected override void Reset()
        {
            cell = null;
            obj = null;
        }

        public bool CanMoveToCell() => obstacles.Count == 0;
    }
}
