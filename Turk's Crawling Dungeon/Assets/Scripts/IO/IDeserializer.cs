using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public interface IDeserializer
    {
        float Progress { get; }

        IEnumerator DeserializeAll();
    }
}
