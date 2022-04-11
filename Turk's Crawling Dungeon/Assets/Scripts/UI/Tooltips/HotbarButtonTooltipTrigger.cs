using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TCD.Objects.Parts;
using TCD.Objects.Parts.Talents;

namespace TCD.UI.Tooltips
{
    public class HotbarButtonTooltipTrigger : TalentTooltipTrigger
    {
        [SerializeField] private HotbarButton button;

        protected override Talent Talent => HotbarButton.talent;

        protected override int TalentLevel => Talent.level;

        private HotbarButton HotbarButton
        {
            get
            {
                if (!button)
                    button = GetComponent<HotbarButton>();
                return button;
            }
        }
    }
}
