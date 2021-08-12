using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TCD
{
    public class TileLoader : ResourceLoader<TileBase>
    {   
        public override string ResourcePath => "Tiles";
    }
}
