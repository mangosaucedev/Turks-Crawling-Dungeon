using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts.Talents;

namespace TCD
{
    public class TalentUsedEvent : Event
    {
        public Talent talent;

        public override string Name => "Talent Used";
    
        public TalentUsedEvent(Talent talent) : base()
        {
            this.talent = talent;
        }
    }
}
