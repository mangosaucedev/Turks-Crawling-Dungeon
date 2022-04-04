using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Attacks;

namespace TCD.IO
{
    [AssetLoader]
    public class DamageTypeCollectionDeserializer : JsonDeserializer<DamageTypeCollection>
    {
        protected override string FullPath => Directories.DamageTypes;

        protected override string Extension => Extensions.DamageType;

        public override void AddAsset(string name, DamageTypeCollection obj)
        {
            foreach (DamageType damageType in obj.damageTypes)
                Assets.Add(damageType.name, damageType);
        }
    }
}
