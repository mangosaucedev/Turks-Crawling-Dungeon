using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Cinematics;

namespace TCD.IO
{
    [AssetLoader]
    public class CinematicCollectionDeserializer : JsonDeserializer<CinematicCollection>
    {
        protected override string FullPath => Directories.Cinematics;

        protected override string Extension => Extensions.Cinematic;

        public override void AddAsset(string name, CinematicCollection obj)
        {
            foreach (Cinematic cinematic in obj.cinematics)
                Assets.Add(cinematic.name, cinematic);
        }
    }
}
