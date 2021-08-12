using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TCD.Objects;
using TCD.UI;

namespace TCD.Zones
{
    public class ZoneResetter : MonoBehaviour
    {
        private bool resetPlayer;

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
            ViewManager.Open("Loading View");
            yield return DestroyAllObjectsRoutine();
            ResetTilemaps();
            yield return GenerateNewZone();
            ViewManager.Close("Loading View");
        }

        private IEnumerator DestroyAllObjectsRoutine()
        {
            Transform parent = ParentManager.Objects;
            int count = parent.childCount;
            for (int i = count - 1; i >= 0; i--)
            {
                if (i % 16 == 0)
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

        private IEnumerator GenerateNewZone()
        {
            ZoneGenerator zoneGenerator = ServiceLocator.Get<ZoneGenerator>();
            yield return zoneGenerator.GenerateZoneRoutine();
        }
    }
}
