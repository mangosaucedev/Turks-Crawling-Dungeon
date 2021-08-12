using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts.Talents
{
    public abstract class TalentRequirement
    {
        public abstract bool MeetsRequirement(BaseObject obj);
    }
}
