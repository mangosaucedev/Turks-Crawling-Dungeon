using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class PlayerBrain : Brain
    {
        public override string Name => "Player Brain";

        public override bool HandleEvent<T>(T e)
        {
            return true;
        }
    }
}
