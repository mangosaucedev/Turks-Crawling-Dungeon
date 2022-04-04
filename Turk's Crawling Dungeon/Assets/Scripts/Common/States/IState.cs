using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public interface IState : IStateMachine
    {
        string Name { get; }

        void Start();

        void End();
    }
}
