using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TCD.Objects;
using TCD.Objects.Parts;
using TCD;

public class ObjectFactoryPropertyTests
{
    [UnityTest]
    public IEnumerator TestPropertyPersistence()
    {
        yield return Startup();
        yield return null;
        ObjectBlueprint blueprint = Assets.Get<ObjectBlueprint>("Liquid");
        GameObject prefab = ObjectFactory.BuildPrefabFromBlueprint(blueprint);
        GameObject instantiatedObject = Object.Instantiate(prefab);
        BaseObject liquid = ObjectFactory.BuildFromBlueprint("Liquid", Vector2Int.zero);
        Tiling3x3 prefabTiling = prefab.GetComponentInChildren<Tiling3x3>();
        Tiling3x3 instantiatedTiling = instantiatedObject.GetComponentInChildren<Tiling3x3>();
        Tiling3x3 gameObjectTiling = liquid.parts.Get<Tiling3x3>();
        Assert.AreEqual("LiquidUpperLeft", prefabTiling.UpperLeft);
        Assert.AreEqual("LiquidUpperLeft", instantiatedTiling.UpperLeft);
        Assert.AreEqual("LiquidUpperLeft", gameObjectTiling.UpperLeft);
    }

    private IEnumerator Startup()
    {
        GameObject objectsParent = new GameObject("--- Objects ---");
        GameObject assetObject = new GameObject("Assets", typeof(Assets), typeof(DebugRawViewer));
        GameStartup gameStartup = new GameStartup();
        yield return gameStartup.StartGame();
        CurrentZoneInfo.grid = new GameGrid(1, 1);
    }

    [UnityTest]
    public IEnumerator TestDisplayInfo()
    {
        yield return Startup();
        yield return null;
        BaseObject obj = ObjectFactory.BuildFromBlueprint("DeadBush", Vector2Int.zero);
        Assert.IsTrue(!string.IsNullOrEmpty(obj.GetDisplayName()));
        Assert.IsTrue(!string.IsNullOrEmpty(obj.GetDisplayNamePlural()));
        Assert.IsTrue(!string.IsNullOrEmpty(obj.GetDescription()));
    }
}
