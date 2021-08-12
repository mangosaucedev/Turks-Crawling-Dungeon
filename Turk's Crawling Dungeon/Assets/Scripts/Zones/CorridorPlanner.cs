using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Zones
{
    public class CorridorPlanner : GeneratorObject
    {
        private List<IChamber> Chambers => Zone.Chambers;
        private IChamber currentChamber;
        private IChamber parentChamber;
        private ChamberAnchor chamberAnchor;
        private ChamberAnchor parentAnchor;
        private List<ChamberAnchor> facingAnchors = new List<ChamberAnchor>();

        public override IEnumerator Generate()
        {
            for (int i = 0; i < Chambers.Count; i++)
            {
                if (i == 0)
                    continue;
                currentChamber = Chambers[i];
                parentChamber = Chambers[i - 1];
                PlanCorridorBetweenChamberAndParent();
                yield return null;
            }
        }

        private void PlanCorridorBetweenChamberAndParent()
        {
            ChooseAnchorFromCurrentChamber();
            ChooseAnchorFromParentChamber();
            ICorridor corridor = CorridorFactory.Build(chamberAnchor, parentAnchor);
            Zone.Corridors.Add(corridor);
            Zone.Features.Add(corridor);
        }

        private void ChooseAnchorFromCurrentChamber()
        {
            facingAnchors.Clear();
            foreach (ChamberAnchor anchor in currentChamber.Anchors)
            {
                if (AnchorFacesParentChamber(anchor))
                    facingAnchors.Add(anchor);
            }
            if (facingAnchors.Count > 0)
                chamberAnchor = Choose.Random(facingAnchors);
            else
                chamberAnchor = Choose.Random(currentChamber.Anchors);
        }

        private bool AnchorFacesParentChamber(ChamberAnchor anchor)
        {
            Cardinal cardinalToParent = CardinalDirectionToParent();
            return anchor.direction == cardinalToParent;
        }

        private Cardinal CardinalDirectionToParent()
        {
            int midX = currentChamber.X + currentChamber.Width / 2;
            int midY = currentChamber.Y + currentChamber.Height / 2;
            int parentMidX = parentChamber.X + parentChamber.Width / 2;
            int parentMidY = parentChamber.Y + parentChamber.Height / 2;
            Vector2 mid = new Vector2(midX, midY);
            Vector2 parentMid = new Vector2(parentMidX, parentMidY);
            Vector2 vectorToParent = parentMid - mid;
            Vector2Int vectorIntToParent = vectorToParent.ToVectorIntNormalized();
            return vectorIntToParent.ToCardinal();
        }

        private void ChooseAnchorFromParentChamber()
        {
            facingAnchors.Clear();
            foreach (ChamberAnchor anchor in parentChamber.Anchors)
            {
                if (ParentAnchorFacesChamber(anchor))
                    facingAnchors.Add(anchor);
            }
            if (facingAnchors.Count > 0)
                parentAnchor = Choose.Random(facingAnchors);
            else
                parentAnchor = Choose.Random(parentChamber.Anchors);
        }

        private bool ParentAnchorFacesChamber(ChamberAnchor anchor)
        {
            Cardinal cardinalToParent = CardinalDirectionToParent();
            Cardinal oppositeCardinal = CardinalTo.Opposite(cardinalToParent);
            return anchor.direction == oppositeCardinal;
        }
    }
}
