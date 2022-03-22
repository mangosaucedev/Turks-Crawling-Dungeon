using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts.Talents;

namespace TCD.UI
{
    public class TalentTreeDisplay : MonoBehaviour
    {
        private TalentTree talentTree;

        public void BuildTalentTree(TalentTree talentTree)
        {
            this.talentTree = talentTree;
        }
    }
}
