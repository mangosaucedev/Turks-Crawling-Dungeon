using System.Collections;
using System.Collections.Generic;
using TCD.Objects.Parts.Talents;
using UnityEngine;

namespace TCD.UI.Tooltips
{
    [RequireComponent(typeof(ITalentButton))]
    public class TalentButtonTooltipTrigger : TalentTooltipTrigger
    {
        private ITalentButton button;

        protected override Talent Talent => Button.Talent;

        protected override int TalentLevel => Button.TalentLevel;

        private ITalentButton Button
        {
            get
            {
                if (button == null)
                    button = GetComponent<ITalentButton>();
                return button;
            }
        }
    }
}
