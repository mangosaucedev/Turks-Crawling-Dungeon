using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;

namespace TCD.Zones
{
    public class StairsPlacer : ZoneGeneratorMachine
    {
        private IFeature currentFeature;

        public override string LoadMessage => "Placing stairs...";

        public override IEnumerator Generate()
        {
            if (Zone.Chambers.Count > 0)
                currentFeature = Zone.Chambers[Zone.Chambers.Count - 1];
            else
                currentFeature = Zone.Features[Zone.Features.Count - 1];
            Vector2Int position = GetRandomPosition();
            ObjectFactory.BuildFromBlueprint("DownStairs", position);
            yield break;
        }

        public Vector2Int GetRandomPosition()
        {
            using (GrabBag<Vector2Int> positions = new GrabBag<Vector2Int>())
            {
                foreach (Vector2Int position in currentFeature.OccupiedPositions)
                {
                    if (PositionChecker.IsEmpty(position))
                        positions.AddItem(position, Mathf.Pow(Vector2Int.Distance(PlayerInfo.currentPlayer.cell.Position, position), 2));
                }
                if (positions.Count == 0)
                    ExceptionHandler.Handle(new Exception("Could not place down stairs in zone: no valid positions for placement!"));
                return positions.Grab();
            }
        }
    }
}
