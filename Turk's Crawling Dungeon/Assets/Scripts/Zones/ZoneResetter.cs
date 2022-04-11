using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TCD.Objects;
using TCD.UI;
using TCD.Zones.Dungeons;

namespace TCD.Zones
{
    public class ZoneResetter : MonoBehaviour
    {
        private const int OBJECTS_BEFORE_PAUSE = 256;

        public bool resetPlayer;

        public static void ResetZone(bool resetPlayer = false)
        {
            PlayerPathChecker.endRoomPosition = Vector2Int.zero;
            ZoneResetter zoneResetter = ServiceLocator.Get<ZoneResetter>();
            zoneResetter.resetPlayer = resetPlayer;
            zoneResetter.StopAllCoroutines();
            zoneResetter.StartCoroutine(zoneResetter.ResetZoneRoutine());
        }

        private IEnumerator ResetZoneRoutine()
        {
            yield return GenerateNewZoneRoutine();
        }

        public void UnloadZone(bool resetPlayer = false)
        {
            StopAllCoroutines();
            LoadingManager loadingManager = ServiceLocator.Get<LoadingManager>();
            ZoneUnloadOperation operation = new ZoneUnloadOperation(UnloadZoneRoutine(resetPlayer));
            loadingManager.EnqueueLoadingOperation(operation);
        }

        public IEnumerator UnloadZoneRoutine(bool resetPlayer = false)
        {
            this.resetPlayer = resetPlayer;
            yield return DestroyAllObjectsRoutine();
            ResetTilemaps();
        }

        private IEnumerator DestroyAllObjectsRoutine()
        {
            Transform parent = ParentManager.Objects;
            int count = parent.childCount;
            for (int i = count - 1; i >= 0; i--)
            {
                if (i % OBJECTS_BEFORE_PAUSE == 0)
                    yield return null;
                if (i > parent.childCount - 1)
                    continue;
                Transform child = parent.GetChild(i);
                if (child.TryGetComponentInParent(out BaseObject obj))
                {
                    if (obj == PlayerInfo.currentPlayer && !resetPlayer)
                        continue;
                    Destroy(obj.gameObject);
                }
            }
        }

        private void ResetTilemaps()
        {
            TilemapManager groundTilemapManager = ServiceLocator.Get<GroundTilemapManager>();
            Tilemap tilemap = groundTilemapManager.Tilemap;
            tilemap.ClearAllTiles();
        }

        private IEnumerator GenerateNewZoneRoutine()
        {
            ZoneGeneratorManager zoneGenerator = ServiceLocator.Get<ZoneGeneratorManager>();
            if (CampaignHandler.currentCampaign != null)
            {
                Dungeon dungeon = DungeonHandler.currentDungeon;
                yield return zoneGenerator.GenerateZoneRoutine(dungeon, DungeonHandler.currentZoneIndex);
            }
            else
                yield return zoneGenerator.GenerateZoneRoutine();
        }
    }
}
