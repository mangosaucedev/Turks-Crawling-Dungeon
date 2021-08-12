using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Attacks;

namespace TCD
{
    public class BeforeAttackEvent : ActOnObjectEvent
    {
        public static new readonly string id = "Before Attack";

        public BaseObject target;
        public Attack attack;
        
        private string result;

        public string Result => string.IsNullOrEmpty(result) ? "failed" : result;

        public override string Id => id;

        ~BeforeAttackEvent() => ReturnToPool();

        protected override void Reset()
        {
            base.Reset();
            target = null;
            attack = null;
            result = "";
        }

        public bool SetResult(string str)
        {
            if (string.IsNullOrEmpty(result))
            {
                result = str;
                return true;
            }
            return false;
        }
    }
}
