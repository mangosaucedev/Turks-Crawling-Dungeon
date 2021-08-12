using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts.Talents;

namespace TCD.Inputs.Hotbar
{
    public class UseHotbarTalent : HotbarAction
    {
        public Talent talent;

        public UseHotbarTalent(Talent talent)
        {
            this.talent = talent;
        }

        public override bool CanUseAction()
        {
            return talent.activeCooldown <= 0;   
        }

        public override bool PerformAction()
        {
            return true;
        }
    }
}
