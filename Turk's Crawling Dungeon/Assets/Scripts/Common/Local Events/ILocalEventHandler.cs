using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public interface ILocalEventHandler 
    {
        bool FireEvent<T>(ILocalEventHandler target, T e) where T : LocalEvent;
        bool HandleEvent<T>(T e) where T : LocalEvent;
    }
}