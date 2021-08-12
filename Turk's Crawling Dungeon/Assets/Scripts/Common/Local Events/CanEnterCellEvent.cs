using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD
{
    public class CanEnterCellEvent : ActOnCellEvent
    {
        public static new readonly string id = "Can Enter Cell";

        public HashSet<BaseObject> obstacles = new HashSet<BaseObject>();
        public HashSet<BaseObject> exceptions = new HashSet<BaseObject>();

        public override string Id => id;

        ~CanEnterCellEvent() => ReturnToPool();

        public bool CanEnterCell()
        {
            foreach (BaseObject obj in obstacles)
            {
                if (!exceptions.Contains(obj))
                    return false;
            }
            return true;
        }

        protected override void Reset()
        {
            base.Reset();
            obstacles.Clear();
            exceptions.Clear();
        }
    }
}
