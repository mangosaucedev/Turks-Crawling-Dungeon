using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public enum FactionOutlook : int
    {
        Unknown = -1001,
        Revengeful = -600,
        Hated = -400,
        Disapproving = -250,
        Disliked = -125,
        Neutral = 125,
        Appreciated = 250,
        Respected = 400,
        Favored = 600,
        Revered = 1000
    }
}
