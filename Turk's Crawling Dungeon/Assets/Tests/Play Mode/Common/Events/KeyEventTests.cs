using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TCD;

public class KeyEventTests
{
    private const int FROM_POOL_TEST_PASSES = 100000;
    private const int FROM_POOL_TEST_PASSES_BEFORE_PAUSE = 100;

    [UnityTest]
    public IEnumerator TestFromPoolCalls100k()
    {
        for (int i = 0; i < FROM_POOL_TEST_PASSES; i++)
        {
            KeyEvent e = KeyEvent.FromPool(default);
            EventManager.Send(e);
            if (i % FROM_POOL_TEST_PASSES_BEFORE_PAUSE == 0)
                yield return null;
        }
    }

    [UnityTest]
    public IEnumerator Test3ConcurrentFromPoolCalls100k()
    {
        for (int i = 0; i < FROM_POOL_TEST_PASSES; i++)
        {
            KeyEvent e1 = KeyEvent.FromPool(default);
            KeyEvent e2 = KeyEvent.FromPool(default);
            KeyEvent e3 = KeyEvent.FromPool(default);
            EventManager.Send(e1);
            EventManager.Send(e2);
            EventManager.Send(e3);
            if (i % FROM_POOL_TEST_PASSES_BEFORE_PAUSE == 0)
                yield return null;
        }
    }
}
