using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts;

namespace TCD.Objects
{
    public class Hp : ObjectComponent, IHp
    {
        public Hp(BaseObject parent) : base(parent)
        {

        }

        public bool ModifyHp(float amount)
        {
            return true;
        }

        public float GetHp()
        {
            return 1;
        }

        public float GetHpMax()
        {
            return 1;
        }
    }
}
