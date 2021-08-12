using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class BeforeEnterCellEvent : ActOnCellEvent
    {
        public static new readonly string id = "Before Enter Cell";
        
        public override string Id => id;

        ~BeforeEnterCellEvent() => ReturnToPool();
    }
}
