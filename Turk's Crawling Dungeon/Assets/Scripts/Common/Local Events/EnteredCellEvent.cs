using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class EnteredCellEvent : ActOnCellEvent
    {
        public static new readonly string id = "Entered Cell";

        public override string Id => id;

        ~EnteredCellEvent() => ReturnToPool();
    }
}
