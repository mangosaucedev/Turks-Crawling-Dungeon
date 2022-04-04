using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public interface IAssetLoader
    {
        float Progress { get; }

        IEnumerator LoadAll();
    }
}
