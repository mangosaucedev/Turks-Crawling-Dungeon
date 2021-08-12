using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class PlayerWallet : Wallet
    {
        public override string Name => "Player Wallet";

        public override void AddMoney(decimal value)
        {
            base.AddMoney(value);
            MessageLog.Add("You got $" + ((float)value).RoundToDecimal(2).ToString());
            ScoreHandler.ModifyScore(Mathf.RoundToInt((float) value * 100));
        }
    }
}
