using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class CanExitCellEvent : ActOnCellEvent
    {
        public static new readonly string id = "Can Exit Cell";

        public override string Id => id;

        ~CanExitCellEvent() => ReturnToPool();
    }
}
