using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TCD.Pathfinding;
using TCD.Zones.Environments;

namespace TCD.Zones
{
    public class ZoneGeneratorManager : MonoBehaviour, IZoneGenerator
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
            GenerateZone(ZoneGeneratorType.Cavern);
        }

        public void GenerateZone(ZoneGeneratorType type)
        {
            if (!hasBegunGeneratingZone)
            {
                hasBegunGeneratingZone = true;
                StopAllCoroutines();
                StartCoroutine(GenerateZoneRoutine(type));
            }
        }

        public IEnumerator GenerateZoneRoutine(ZoneGeneratorType type)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string zone = "";
            switch (type)
            {
                case ZoneGeneratorType.Cavern:
                    zone = "Cavern";
                    break;
                default:
                    zone = Choose.Random("Level0", "Overgrown", "Decomposition", "Fungus");
                    break;
            }
             
            CurrentZoneInfo.zone = ZoneFactory.BuildFromBlueprint(zone);
            CurrentZoneInfo.grid = 
                new GameGrid(CurrentZoneInfo.zone.Width, CurrentZoneInfo.zone.Height);
            CurrentZoneInfo.navGrid =
                new NavGrid(CurrentZoneInfo.zone.Width, CurrentZoneInfo.zone.Height);

            ZoneGenerator zoneGenerator = new GenericZoneGenerator();
            switch (type)
            {
                case ZoneGeneratorType.Cavern:
                    zoneGenerator = new CavernZoneGenerator();
                    break;
                default:
                    break;
            }
            yield return zoneGenerator.GenerateZoneRoutine();

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
