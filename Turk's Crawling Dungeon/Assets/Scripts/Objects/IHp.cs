using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects
{
    public interface IHp 
    {
        bool ModifyHp(float amount);

        float GetHp();

        float GetHpMax();
    }
}
