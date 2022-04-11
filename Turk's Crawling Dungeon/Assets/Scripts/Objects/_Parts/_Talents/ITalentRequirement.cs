using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts.Talents
{
    public interface ITalentRequirement
    {
        bool MeetsRequirement();

        bool MeetsEmbarkRequirement();

        string GetDescription();
    }
}
