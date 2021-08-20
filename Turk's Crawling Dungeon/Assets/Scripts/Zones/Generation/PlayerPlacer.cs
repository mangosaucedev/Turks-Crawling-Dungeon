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
    public class PlayerPlacer : GeneratorObject
    {
        private Tilemap ground;

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
                BaseObject player = ObjectFactory.BuildFromBlueprint("Player", position);
                PlayerInfo.currentPlayer = player;
                EventManager.Send(new PlayerCreatedEvent());
            }         
            else
                PlayerInfo.currentPlayer.cell.SetPosition(position);
            yield return null;
        }

        private Vector2Int FindSuitableSpawnForPlayer()
        {
            IChamber firstChamber = CurrentZoneInfo.zone.Chambers[0];
            int xMin = firstChamber.X;
            int xMax = xMin + firstChamber.Width;
            int yMin = firstChamber.Y;
            int yMax = yMin + firstChamber.Height;
            List<Vector2Int> validSpawns = new List<Vector2Int>();
            for (int x = xMin; x < xMax; x++)
                for (int y = yMin; y < yMax; y++)
                {
                    if (IsValidSpawn(x, y))
                        validSpawns.Add(new Vector2Int(x, y));
                }
            if (validSpawns.Count > 0)
                return validSpawns[Random.Range(0, validSpawns.Count)];
            throw new Exception("Cannot place player: no valid positions to spawn them!");
        }

        private bool IsValidSpawn(int x, int y) => PositionChecker.IsEmpty(x, y) && PositionChecker.IsFloored(x, y);

    }
}
