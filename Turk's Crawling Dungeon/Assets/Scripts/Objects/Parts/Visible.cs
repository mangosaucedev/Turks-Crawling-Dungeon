using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class Visible : Part
    {
        [SerializeField] private int visibilityRadius;

        public override string Name => "Visible";

        public int VisibilityRadius
        {
            get => visibilityRadius;
            set => visibilityRadius = value;
        }

        public bool IsVisibleToPlayer() => (FieldOfView.IsVisible(Position) && 
                (VisibilityRadius == 0 || PlayerInfo.GetDistanceToPlayer(parent) <= VisibilityRadius));
        
    }
}
