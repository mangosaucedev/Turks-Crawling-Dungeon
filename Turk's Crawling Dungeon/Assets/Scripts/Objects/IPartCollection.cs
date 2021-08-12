using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects
{
    public interface IPartCollection
    {
        Transform PartsParent { get; }

        List<Part> Parts { get; }

        Part Add(Type type);

        bool Remove(Type type);

        bool Has(Type type);

        Part Get(Type type);

        T Get<T>() where T : Part;

        bool TryGet<T>(out T part) where T : Part;
    }
}
