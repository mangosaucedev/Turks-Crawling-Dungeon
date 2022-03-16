using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public interface IDeserializer
    {
        IEnumerator DeserializeAll();
    }
}
