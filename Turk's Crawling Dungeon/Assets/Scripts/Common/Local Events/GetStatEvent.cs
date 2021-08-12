using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD
{
    public class GetStatEvent : ActOnObjectEvent
    {
        public Stat stat;
        public int baseLevel;
        public int level;

        public static new readonly string id = "GetStat";

        public override string Id => id;

        ~GetStatEvent() => ReturnToPool();

        protected override void Reset()
        {
            stat = Stat.Strength;
            base.Reset();
        }
    }
}