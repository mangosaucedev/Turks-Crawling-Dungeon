using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Blueprints;

namespace TCD.IO
{
    [AssetDeserializer]
    public class ObjectBlueprint_v2CollectionDeserializer : JsonDeserializer<ObjectBlueprint_v2Collection>
    {
        protected override string FullPath => Directories.Objects;

        protected override string Extension => Extensions.Object;

        public override void AddAsset(string name, ObjectBlueprint_v2Collection obj)
        {
            foreach (ObjectBlueprint_v2 blueprint in obj.blueprints)
                Assets.Add(blueprint.name, blueprint);
        }
    }
}
