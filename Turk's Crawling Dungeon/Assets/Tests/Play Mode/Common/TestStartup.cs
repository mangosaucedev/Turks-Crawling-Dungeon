using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD;
using TCD.Objects;

public static class TestStartup 
{
    private static bool hasStartedUp;

    public static IEnumerator Startup()
    {
        if (hasStartedUp)
            yield break;
        new GameObject("--- Objects ---");
        new GameObject("Assets", typeof(Assets), typeof(DebugRawViewer));
        yield return GameStartup.StartGame();
        CurrentZoneInfo.grid = new GameGrid(10, 10);
        PlayerInfo.currentPlayer = ObjectFactory.BuildFromBlueprint("Player", Vector2Int.zero);
        hasStartedUp = true;
    }
}
