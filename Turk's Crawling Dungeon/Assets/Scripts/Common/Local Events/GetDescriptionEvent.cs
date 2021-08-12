using TCD.Objects;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace TCD
{
    public class GetDescriptionEvent : ObjectEvent
    {
        public static new readonly string id = "Get Description";

        public BaseObject obj;
        public string description;
        private StringBuilder stringBuilder = new StringBuilder();

        public override BaseObject Object => obj;

        public override string Id => id;

        ~GetDescriptionEvent() => ReturnToPool();

        public string GetDescription()
        {
            return description;
        }

        public string GetDescriptionShort()
        {
            return description;
        }

        protected override void Reset()
        {
            obj = null;
            description = "";
            stringBuilder.Clear();
        }
    }
}
