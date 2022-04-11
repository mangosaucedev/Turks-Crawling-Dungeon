using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class EmbarkTalentPointModifiedEvent : Event
    {
        public Type type;
        public int level;

        public override string Name => "Embark Talent Point Modified";

        public EmbarkTalentPointModifiedEvent(Type type, int level) : base()
        {
            this.type = type;
            this.level = level;
        }
    }
}
