using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public struct Noise : IComparable<Noise>
    {
        public int volume;
        public NoiseType type;

        public int CompareTo(Noise other)
        {
            if (type == other.type)
                return volume.CompareTo(other.volume);
            return type.CompareTo(other.type);
        }
    }
}
