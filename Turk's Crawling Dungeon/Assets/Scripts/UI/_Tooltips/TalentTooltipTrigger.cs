using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;
using TCD.Objects.Parts.Talents;

namespace TCD.UI.Tooltips
{
    public abstract class TalentTooltipTrigger : TooltipTrigger
    {

        protected abstract Talent Talent { get; }

        protected abstract int TalentLevel { get; }

        public override string GetHeader()
        {
            return TalentUtility.BuildTalentTitle(Talent);
        }

        public override string GetBody()
        {
            return TalentUtility.BuildTalentDescription(Talent, TalentLevel);
        }
    }
}
