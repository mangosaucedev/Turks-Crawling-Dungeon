using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TCD;
using TCD.Objects;
using TCD.Objects.Parts;

public class EquipmentTests
{
    private Equipment playerEquipment;
    private Equippable oneHandedEquippable;
    private Equippable oneHandedEquippableB;
    private Equippable oneHandedEquippableC;
    private Equippable twoHandedEquippable;
    private Equippable twoHandedEquippableB;

    [UnityTest]
    public IEnumerator TestTryEquipOneHandedSuccessful()
    {
        yield return TestStartup.Startup();
        SetupEquipment();
        yield return SetupOneHandedEquippable();
        Assert.IsTrue(playerEquipment.TryEquip(oneHandedEquippable.parent));
        Assert.IsTrue(oneHandedEquippable.IsEquippedToPlayer);
    }

    private void SetupEquipment()
    {
        if (playerEquipment)
            return;
        playerEquipment = (Equipment) PlayerInfo.currentPlayer.parts.Add(typeof(Equipment));
        PlayerInfo.currentPlayer.parts.Add(typeof(Inventory));
    }

    private IEnumerator SetupOneHandedEquippable()
    {
        BaseObject obj = ObjectFactory.BuildFromBlueprint("RustyHatchet", Vector2Int.one);
        oneHandedEquippable = obj.parts.Get<Equippable>();
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestTryEquipTwoHandedSuccessful()
    {
        yield return TestStartup.Startup();
        SetupEquipment();
        yield return SetupTwoHandedEquippable();
        Assert.IsTrue(playerEquipment.TryEquip(twoHandedEquippable.parent));
        Assert.IsTrue(twoHandedEquippable.IsEquippedToPlayer);
    }

    private IEnumerator SetupTwoHandedEquippable()
    {
        BaseObject obj = ObjectFactory.BuildFromBlueprint("RottingWoodScepter", Vector2Int.one);
        twoHandedEquippable = obj.parts.Get<Equippable>();
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestTryEquipSwapOutTwoHandedWithTwoOneHanded()
    {
        yield return TestStartup.Startup();
        SetupEquipment();
        yield return SetupTwoHandedEquippable();
        yield return SetupOneHandedEquippableBC();
        Assert.IsTrue(playerEquipment.TryEquip(twoHandedEquippable.parent));
        Assert.IsTrue(playerEquipment.TryEquip(oneHandedEquippableB.parent));
        Assert.IsTrue(playerEquipment.TryEquip(oneHandedEquippableC.parent));
        Assert.IsFalse(twoHandedEquippable.IsEquippedToPlayer);
        Assert.IsTrue(oneHandedEquippableB.IsEquippedToPlayer);
        Assert.IsTrue(oneHandedEquippableC.IsEquippedToPlayer);
    }

    private IEnumerator SetupOneHandedEquippableBC()
    {
        BaseObject obj = ObjectFactory.BuildFromBlueprint("RustyHatchet", Vector2Int.one);
        oneHandedEquippableB = obj.parts.Get<Equippable>();
        obj = ObjectFactory.BuildFromBlueprint("RustyHatchet", Vector2Int.one);
        oneHandedEquippableC = obj.parts.Get<Equippable>();
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestTryEquipSwapOutTwoHandedWithTwoHanded()
    {
        yield return TestStartup.Startup();
        SetupEquipment();
        yield return SetupTwoHandedEquippable();
        yield return SetupTwoHandedEquippableB();
        Assert.IsTrue(playerEquipment.TryEquip(twoHandedEquippable.parent));
        Assert.IsTrue(playerEquipment.TryEquip(twoHandedEquippableB.parent));
        Assert.IsFalse(twoHandedEquippable.IsEquippedToPlayer);
        Assert.IsTrue(twoHandedEquippableB.IsEquippedToPlayer);
    }

    private IEnumerator SetupTwoHandedEquippableB()
    {
        BaseObject obj = ObjectFactory.BuildFromBlueprint("RottingWoodScepter", Vector2Int.one);
        twoHandedEquippableB = obj.parts.Get<Equippable>();
        yield return null;
    }


    [UnityTest]
    public IEnumerator TestTryEquipSwapOutOneHandedWithTwoHanded()
    {
        yield return TestStartup.Startup();
        SetupEquipment();
        yield return SetupOneHandedEquippable();
        yield return SetupTwoHandedEquippable();
        Assert.IsTrue(playerEquipment.TryEquip(oneHandedEquippable.parent));
        Assert.IsTrue(playerEquipment.TryEquip(twoHandedEquippable.parent));
        Assert.IsFalse(oneHandedEquippable.IsEquippedToPlayer);
        Assert.IsTrue(twoHandedEquippable.IsEquippedToPlayer);
    }

    [UnityTest]
    public IEnumerator TestTryEquipSwapOutOneHandedWithTwoOneHanded()
    {
        yield return TestStartup.Startup();
        SetupEquipment();
        yield return SetupOneHandedEquippable();
        yield return SetupOneHandedEquippableBC();
        Assert.IsTrue(playerEquipment.TryEquip(oneHandedEquippable.parent));
        Assert.IsTrue(playerEquipment.TryEquip(oneHandedEquippableB.parent));
        Assert.IsTrue(playerEquipment.TryEquip(oneHandedEquippableC.parent));
        Assert.IsFalse(oneHandedEquippable.IsEquippedToPlayer);
        Assert.IsTrue(oneHandedEquippableB.IsEquippedToPlayer);
        Assert.IsTrue(oneHandedEquippableC.IsEquippedToPlayer);
    }
}
