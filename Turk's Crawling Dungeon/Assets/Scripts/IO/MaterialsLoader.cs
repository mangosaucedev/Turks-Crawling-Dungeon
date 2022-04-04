using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.IO
{
    [AssetLoader]
    public class MaterialsLoader : ResourceLoader<Material>
    {
        public override string ResourcePath => "Materials";
    }
}
