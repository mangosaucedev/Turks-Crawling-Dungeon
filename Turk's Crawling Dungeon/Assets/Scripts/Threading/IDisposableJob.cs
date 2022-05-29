using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

namespace TCD.Threading
{
    public interface IDisposableJob : IDisposable, IJob
    {
        Guid Guid { get; } 
    }
}
