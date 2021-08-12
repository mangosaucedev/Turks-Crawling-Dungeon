using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TCD.Objects;

namespace TCD
{
    public class GetDisplayNameEvent : ObjectEvent
    {
        public static new readonly string id = "Get Description";

        public BaseObject obj;
        public string displayName;
        public string displayNamePlural;
        public bool doNotIncludePrefix;
        public bool doNotIncludeSuffix;
        private StringBuilder prefixBuilder = new StringBuilder();
        private StringBuilder nameBuilder = new StringBuilder();
        private StringBuilder suffixBuilder = new StringBuilder();

        public override BaseObject Object => obj;

        public override string Id => id;

        ~GetDisplayNameEvent() => ReturnToPool();

        public string GetDisplayName()
        {
            return displayName;
        }

        public string GetDisplayNamePlural()
        {
            return displayNamePlural;
        }

        protected override void Reset()
        {
            obj = null;
            displayName = "";
            displayNamePlural = "";
            doNotIncludePrefix = false;
            doNotIncludeSuffix = false;
            prefixBuilder.Clear();
            nameBuilder.Clear();
            suffixBuilder.Clear();
        }
    }
}
