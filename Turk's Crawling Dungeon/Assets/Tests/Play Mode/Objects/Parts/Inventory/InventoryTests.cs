using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TCD;
using TCD.Objects;
using TCD.Objects.Parts;

public class InventoryTests
{
    private Inventory playerInventory;
    private BaseObject currentObject;
    private Item currentItem;

    [UnityTest]
    public IEnumerator TestTryAddItemSuccessful()
    {
        yield return TestStartup.Startup();
        SetupInventory();
        MakeItem(ref currentObject, ref currentItem, "JadePsyscourge");
        yield return null;
        Assert.IsTrue(playerInventory.TryAddItem(currentObject));
        Assert.IsTrue(currentItem.IsInPlayerInventory);
    }

    private void SetupInventory()
    {
        if (playerInventory)
            return;
        playerInventory = (Inventory) PlayerInfo.currentPlayer.Parts.Add(typeof(Inventory));
    }

    private void MakeItem(ref BaseObject obj, ref Item item, string itemName)
    {
        obj = ObjectFactory.BuildFromBlueprint(itemName, Vector2Int.zero);
        item = obj.Parts.Get<Item>();
    }

    [UnityTest]
    public IEnumerator TestTryAddItemFailOverweight()
    {
        yield return TestStartup.Startup();
        SetupInventory();
        int overweightItemCount = Mathf.FloorToInt(playerInventory.GetMaxWeight());
        for (int i = 0; i < overweightItemCount; i++)
        {
            MakeItem(ref currentObject, ref currentItem, "JadePsyscourge");
            yield return null;
            Assert.IsTrue(playerInventory.TryAddItem(currentObject));
            Assert.IsTrue(currentItem.IsInPlayerInventory);
        }
        MakeItem(ref currentObject, ref currentItem, "JadePsyscourge");
        yield return null;
        Assert.IsFalse(playerInventory.TryAddItem(currentObject));
        Assert.IsFalse(currentItem.IsInPlayerInventory);
    }

}
