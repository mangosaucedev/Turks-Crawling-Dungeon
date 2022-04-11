using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts.Talents;

namespace TCD
{
    public class TalentPointModifiedEvent : Event
    {
        public Talent talent;
        public int level;

        public override string Name => "Talent Point Modified";

        public TalentPointModifiedEvent(Talent talent, int level)
        {
            this.talent = talent;
            this.level = level;
        }
    }
}
