using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class ExitedCellEvent : ActOnCellEvent
    {
        public static new readonly string id = "Exited Cell";

        public override string Id => id;

        ~ExitedCellEvent() => ReturnToPool();
    }
}
