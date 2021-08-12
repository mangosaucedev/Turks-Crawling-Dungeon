using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class Wallet : Part
    {
        public decimal money;

        public override string Name => "Wallet";

        public virtual void AddMoney(decimal value)
        {
            if (value >= 0)
                money += value;
        }
    }
}
