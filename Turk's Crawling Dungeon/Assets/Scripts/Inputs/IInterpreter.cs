using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Inputs
{
    public interface IInterpreter 
    { 
        InputGroup InputGroup { get; }

        void Update();
    }
}
