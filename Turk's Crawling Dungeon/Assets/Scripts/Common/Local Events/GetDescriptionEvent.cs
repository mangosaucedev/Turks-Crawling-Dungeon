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
        private StringBuilder prefixBuilder = new StringBuilder();
        private StringBuilder suffixBuilder = new StringBuilder();

        public override BaseObject Object => obj;

        public override string Id => id;

        ~GetDescriptionEvent() => ReturnToPool();

        public string GetDescription()
        {
            if (prefixBuilder.Length > 0)
                prefixBuilder.Append("\n");
            return prefixBuilder.ToString() + " " + description + " " + suffixBuilder.ToString();
        }

        public string GetDescriptionShort()
        {
            return description;
        }

        public void AddToPrefix(string str)
        {
            if (prefixBuilder.Length > 0)
                prefixBuilder.Append("\n");
            prefixBuilder.Append(str);
        }

        public void AddToSuffix(string str)
        {
            suffixBuilder.Append("\n");
            suffixBuilder.Append(str);
        }

        protected override void Reset()
        {
            obj = null;
            description = "";
            prefixBuilder.Clear();
            suffixBuilder.Clear();
        }
    }
}
