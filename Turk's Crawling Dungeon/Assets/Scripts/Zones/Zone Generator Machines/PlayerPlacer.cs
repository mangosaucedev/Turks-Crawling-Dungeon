using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TCD.Objects;
using TCD.Objects.Parts.Effects;
using Random = UnityEngine.Random;

namespace TCD.Zones
{
    public class PlayerPlacer : ZoneGeneratorMachine
    {
        private Tilemap ground;

        public override string LoadMessage => "Placing player...";

        private Tilemap Ground
        {
            get
            {
                if (!ground)
                {
                    GroundTilemapManager manager = ServiceLocator.Get<GroundTilemapManager>();
                    ground = manager.Tilemap;
                }
                return ground;
            }
        }

        public override IEnumerator Generate()
        {
            Vector2Int position = FindSuitableSpawnForPlayer();
            if (!PlayerInfo.currentPlayer)
            {
                PlayerUtility.BuildPlayer("Player", position);
                EventManager.Send(new PlayerCreatedEvent());
            }         
            else
                PlayerInfo.currentPlayer.cell.SetPosition(position);
            PlayerInfo.currentPlayer.enabled = true;
            yield return null;
        }

        private Vector2Int FindSuitableSpawnForPlayer()
        {
            IFeature firstFeature = CurrentZoneInfo.zone.Features[0];
            int xMin = firstFeature.BoundsInt.xMin;
            int xMax = xMin + firstFeature.BoundsInt.xMax;
            int yMin = firstFeature.BoundsInt.yMin;
            int yMax = yMin + firstFeature.BoundsInt.yMax;
            List<Vector2Int> validSpawns = new List<Vector2Int>();
            foreach(Vector2Int position in firstFeature.OccupiedPositions)
            { 
                if (IsValidSpawn(position.x, position.y))
                    validSpawns.Add(position);
            }
            if (validSpawns.Count > 0)
                return validSpawns[Random.Range(0, validSpawns.Count)];
            ExceptionHandler.Handle(new Exception("Cannot place player: no valid positions to spawn them!"));
            return default;
        }

        private bool IsValidSpawn(int x, int y) => PositionChecker.IsEmpty(x, y);

    }
}
