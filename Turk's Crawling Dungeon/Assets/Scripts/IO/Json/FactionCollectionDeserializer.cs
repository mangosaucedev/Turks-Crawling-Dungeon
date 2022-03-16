using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD.IO
{
    [AssetDeserializer]
    public class FactionCollectionDeserializer : JsonDeserializer<FactionCollection>
    {
        protected override string FullPath => Directories.Factions;

        protected override string Extension => Extensions.Faction;

        public override void AddAsset(string name, FactionCollection obj)
        {
            foreach (Faction faction in obj.factions)
                Assets.Add(faction.name, faction);
        }
    }
}
