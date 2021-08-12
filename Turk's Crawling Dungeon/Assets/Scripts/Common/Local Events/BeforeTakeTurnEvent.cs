using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD.TurksCrawlingDungeon
{
    public class BeforeTakeTurnEvent : ObjectEvent
    {
        public BaseObject obj;

        public static new readonly string id = "Before Take Turn";

        public override BaseObject Object => obj;

        public override string Id => id;

        ~BeforeTakeTurnEvent() => ReturnToPool();

        protected override void Reset()
        {
            obj = null;
        }
    }
}
