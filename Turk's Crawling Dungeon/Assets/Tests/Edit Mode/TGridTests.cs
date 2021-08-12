using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TCD;

public class TGridTests
{
    [Test]
    public void TestGridEnumeration()
    {
        int i = 0;
        TGrid<bool> grid = new TGrid<bool>(10, 10);
        foreach (bool b in grid)
        {
            Assert.IsFalse(b);
            i++;
        }
        Assert.AreEqual(99, i);
    }
}
