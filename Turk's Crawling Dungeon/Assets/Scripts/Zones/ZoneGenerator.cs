using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TCD.Pathfinding;
using TCD.Zones.Environments;

namespace TCD.Zones
{
    public class ZoneGenerator : MonoBehaviour, IZoneGenerator
    {
        private bool hasBegunGeneratingZone;

        [SerializeField] private ZoneParams zoneParams;

#if UNITY_EDITOR
        [Header("Debug")]
        [SerializeField] private bool showDebugInfo;
#endif

        public IZoneParams ZoneParams => zoneParams;

        private void OnEnable()
        {
            EventManager.Listen<GameStartupFinishedEvent>(this, OnGameStartupFinished);
        }

        private void OnDisable()
        {
            EventManager.StopListening<GameStartupFinishedEvent>(this);
        }

        private void OnGameStartupFinished(GameStartupFinishedEvent e)
        {
            EventManager.StopListening<GameStartupFinishedEvent>(this);
            GenerateZone();
        }

        public void GenerateZone()
        {
            if (!hasBegunGeneratingZone)
            {
                hasBegunGeneratingZone = true;
                StopAllCoroutines();
                StartCoroutine(GenerateZoneRoutine());
            }
        }

        public IEnumerator GenerateZoneRoutine()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string zone = Choose.Random("Level0", "Overgrown", "Decomposition", "Fungus");
            CurrentZoneInfo.zone = ZoneFactory.BuildFromBlueprint(zone);
            CurrentZoneInfo.grid = 
                new GameGrid(CurrentZoneInfo.zone.Width, CurrentZoneInfo.zone.Height);
            CurrentZoneInfo.navGrid =
                new NavGrid(CurrentZoneInfo.zone.Width, CurrentZoneInfo.zone.Height);

            ChamberPlanner chamberPlanner = new ChamberPlanner();
            yield return chamberPlanner.Generate();

            CorridorPlanner corridorPlanner = new CorridorPlanner();
            yield return corridorPlanner.Generate();

            FeatureFloorPlanner featureFloorPlanner = new FeatureFloorPlanner();
            yield return featureFloorPlanner.Generate();

            //if (CurrentZoneInfo.zone.ZoneParams.GenerateCaves)
            //{
                //CavePlanner cavePlanner = new CavePlanner();
                //yield return cavePlanner.Generate();
            //}

            WallPlanner wallPlanner = new WallPlanner();
            yield return wallPlanner.Generate();

            DoorPlanner doorPlanner = new DoorPlanner();
            yield return doorPlanner.Generate();

            ZoneBuilder zoneBuilder = new ZoneBuilder();
            yield return zoneBuilder.Generate();

            ZoneColorer zoneColorer = new ZoneColorer();
            yield return zoneColorer.Generate();

            EnvironmentPlanner environmentPlanner = new EnvironmentPlanner();
            yield return environmentPlanner.Generate();

            PlayerPlacer playerPlacer = new PlayerPlacer();
            yield return playerPlacer.Generate();

            StairsPlacer stairsPlacer = new StairsPlacer();
            yield return stairsPlacer.Generate();

            EnvironmentFurnisher environmentFurnisher = new EnvironmentFurnisher();
            yield return environmentFurnisher.Generate();

            ZonePopulator zonePopulator = new ZonePopulator();
            yield return zonePopulator.Generate();

            ZoneLootPlanner zoneLootPlanner = new ZoneLootPlanner();
            yield return zoneLootPlanner.Generate();

            stopwatch.Stop();
            DebugLogger.Log($"Zone generated in {stopwatch.ElapsedMilliseconds} ms.");
            hasBegunGeneratingZone = false;
            EventManager.Send(new ZoneGenerationFinishedEvent());
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!showDebugInfo)
                return;
            IZone zone = CurrentZoneInfo.zone;
            if (zone == null || zone.Chambers == null)
                return;
            for (int i = 0; i < zone.Chambers.Count; i++)
            {
                IChamber chamber = zone.Chambers[i];
                int midX = chamber.X + chamber.Width / 2;
                int midY = chamber.Y + chamber.Height / 2;
                int width = chamber.Width / 4;
                int height = chamber.Height / 4;
                Color color = Color.yellow;
                color.a = 0.3f;
                Gizmos.color = color;
                Vector3 position = new Vector3(midX, midY) * 2;
                Vector3 size = new Vector3(width, height) * 2;
                Gizmos.DrawCube(position, size);
                if (i != 0)
                {
                    IChamber parent = zone.Chambers[i - 1];
                    int parentMidX = parent.X + parent.Width / 2;
                    int parentMidY = parent.Y + parent.Height / 2;
                    Vector3 parentPosition = new Vector3(parentMidX, parentMidY) * 2;
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(position, parentPosition);
                }
            }
        }
#endif
    }
}
