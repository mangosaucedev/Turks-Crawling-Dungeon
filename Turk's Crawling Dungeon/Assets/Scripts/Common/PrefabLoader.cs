using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.IO
{
    [AssetLoader]
    public class PrefabLoader : ResourceLoader<GameObject>
    {
        public override string ResourcePath => "Prefabs";
    }
}
