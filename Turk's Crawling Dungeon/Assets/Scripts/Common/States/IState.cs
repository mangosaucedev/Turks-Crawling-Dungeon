using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public interface IState 
    {
        string Name { get; }

        void Start();
        
        void Update();

        void FixedUpdate();

        void SlowUpdate();

        void End();
    }
}
