using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD
{
    public class UpdateTilingEvent : ActOnCellEvent
    {
        public static new readonly string id = "Update Tiling";

        public Tiling tiling;

        public override string Id => id;

        ~UpdateTilingEvent() => ReturnToPool();

        public bool IsEqualTo(Tiling original) => original.Single == tiling.Single;
    }
}
