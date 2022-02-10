using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Encounters;
using TCD.Pathfinding;

namespace TCD.Zones
{
    public class ZonePopulator : ZoneGeneratorMachine
    {
        private int MIN_ENEMY_DISTANCE_TO_PLAYER = 18;
        private int MAX_ATTEMPTS_TO_POPULATE = 64;
        private int ENEMY_POPULATE_CHANCE = 3;

        private IChamber currentChamber;
        private IFeature currentFeature;
        private Encounter currentEncounter;

        private ZoneEncounters ZoneEncounters => Zone.ZoneEncounters;

        public override IEnumerator Generate()
        {
            PopulateZoneWithForcedEncounters();
            yield return PopulateZoneWithEnemyPatrols();
            yield return PopulateZoneWithEncounters(EncounterType.OpenLoot);
            yield return PopulateZoneWithEncounters(EncounterType.Random);
        }

        private void PopulateZoneWithForcedEncounters()
        {
            foreach (Encounter encounter in ZoneEncounters.GetForcedEncounters())
            {
                currentEncounter = encounter;
                PopulateRandomFeatureWithEncounter();
            }
        }

        private void PopulateRandomFeatureWithEncounter()
        {
            currentFeature = GetRandomFeature();
            int i = 0;
            while (!PopulateFeature())
            {
                currentFeature = GetRandomFeature();
                i++;
                if (i > MAX_ATTEMPTS_TO_POPULATE)
                    ExceptionHandler.Handle(new Exception("Could not populate any feature with encounter " + currentEncounter.name + "!"));
            }
        }

        private IFeature GetRandomFeature()
        {
            int index = RandomInfo.Random.Next(0, Zone.Features.Count - 1);
            return Zone.Features[index];
        }

        private bool PopulateFeature()
        {
            if (currentEncounter == null || !TryFindEmptyPositionInFeature(out Vector2Int position))
                return false;
            currentEncounter.BuildAtPosition(position);
            ZoneEncounters.FlagEncounterAsPlaced(currentEncounter);
            return true;
        }

        private IEnumerator PopulateZoneWithEnemyPatrols()
        {
            int maxRandom = Mathf.Max(ENEMY_POPULATE_CHANCE, Mathf.CeilToInt(ZoneEncounters.density));
            foreach (IFeature feature in Zone.Features)
            { 
                if (RandomInfo.Random.Next(0, maxRandom) < ZoneEncounters.density)
                {
                    currentFeature = feature;
                    if (!TryFindUnobstructedPositionInFeatureClosestToPlayer(out Vector2Int closestPointToPlayer) ||
                        !PlayerPathChecker.TryGetValidPathToPoint(closestPointToPlayer, out NavAstarPath path) ||
                        path.path.Count < MIN_ENEMY_DISTANCE_TO_PLAYER)
                        continue;
                    currentEncounter = ZoneEncounters.GetEncounter(EncounterType.EnemyPatrol, PlayerInfo.GetTier());
                    PopulateFeature();
                    yield return null;
                }
            }
        }

        private IEnumerator PopulateZoneWithEncounters(EncounterType type)
        {
            int passes = Mathf.CeilToInt(Zone.Features.Count * ZoneEncounters.density);
            for (int i = 0; i < passes; i++)
            {
                currentEncounter = ZoneEncounters.GetEncounter(type, PlayerInfo.GetTier());
                if (currentEncounter != null)
                    PopulateRandomFeatureWithEncounter();
                yield return null;
            }
        }


        private bool TryFindEmptyPositionInFeature(out Vector2Int position)
        {
            position = Vector2Int.zero;
            List<Vector2Int> emptyPositions = new List<Vector2Int>();
            int xMin = currentFeature.BoundsInt.xMin;
            int yMin = currentFeature.BoundsInt.yMin;
            int xMax = currentFeature.BoundsInt.xMax;
            int yMax = currentFeature.BoundsInt.yMax;
            for (int x = xMin; x < xMax; x++)
                for (int y = yMin; y < yMax; y++)
                {
                    if (PositionChecker.IsEmpty(x, y) && PositionChecker.IsFloored(x, y))
                        emptyPositions.Add(new Vector2Int(x, y));
                }
            if (emptyPositions.Count == 0)
                return false;
            position = Choose.Random(emptyPositions);
            return true;
        }

        private bool TryFindUnobstructedPositionInFeatureClosestToPlayer(out Vector2Int position)
        {
            position = Vector2Int.zero;
            float minDistance = float.MaxValue;
            int xMin = currentFeature.BoundsInt.xMin;
            int yMin = currentFeature.BoundsInt.yMin;
            int xMax = currentFeature.BoundsInt.xMax;
            int yMax = currentFeature.BoundsInt.yMax;
            for (int x = xMin; x < xMax; x++)
                for (int y = yMin; y < yMax; y++)
                {
                    Vector2Int checkPosition = new Vector2Int(x, y);
                    float distance = PlayerInfo.GetDistanceToPlayer(checkPosition);
                    if (!PositionChecker.IsObstacle(x, y) && PositionChecker.IsFloored(x, y) && distance < minDistance)
                    {
                        position = checkPosition;
                        minDistance = distance;
                    }
                }
            if (position == Vector2Int.zero)
                return false;
            return true;
        }
    }
}
