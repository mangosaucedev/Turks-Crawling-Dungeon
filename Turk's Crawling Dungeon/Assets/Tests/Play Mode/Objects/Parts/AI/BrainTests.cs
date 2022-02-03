using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.TestTools;
using TCD;
using TCD.Objects;
using TCD.TimeManagement;
using TCD.Pathfinding;

public class BrainTests
{
    [UnityTest]
    public IEnumerator TestEnemyUnableToPathToPlayerDoesNotCauseInfiniteLoop()
    {
        new GameObject("--- Objects ---");
        new GameObject("Main Canvas");
        new GameObject("Assets", typeof(Assets), typeof(DebugRawViewer));
        new GameObject("FogOfWarTilemap", typeof(FogOfWarTilemapManager), typeof(Tilemap));
        new GameObject("AudioPlayer", typeof(AudioPlayer), typeof(AudioSource));
        yield return GameStartup.StartGame();
        CurrentZoneInfo.grid = new GameGrid(4, 4);
        CurrentZoneInfo.navGrid = new NavGrid(4, 4);
        PlayerInfo.currentPlayer = ObjectFactory.BuildFromBlueprint("Player", new Vector2Int(1, 1));
        ObjectFactory.BuildFromBlueprint("Wall", new Vector2Int(1, 1));
        ObjectFactory.BuildFromBlueprint("Wall", new Vector2Int(2, 1));
        ObjectFactory.BuildFromBlueprint("Wall", new Vector2Int(3, 1));
        ObjectFactory.BuildFromBlueprint("Wall", new Vector2Int(1, 2));
        ObjectFactory.BuildFromBlueprint("Wall", new Vector2Int(3, 2));
        ObjectFactory.BuildFromBlueprint("Wall", new Vector2Int(1, 3));
        ObjectFactory.BuildFromBlueprint("Wall", new Vector2Int(2, 3));
        ObjectFactory.BuildFromBlueprint("Wall", new Vector2Int(3, 3));
        ObjectFactory.BuildFromBlueprint("GenericEnemy", new Vector2Int(2, 2));
        yield return null;
        TimeScheduler.Tick(100);
        yield return null;
        TimeScheduler.Tick(100);
        yield return null;
        TimeScheduler.Tick(100);
        yield return null;
        TimeScheduler.Tick(100);
    }
}
