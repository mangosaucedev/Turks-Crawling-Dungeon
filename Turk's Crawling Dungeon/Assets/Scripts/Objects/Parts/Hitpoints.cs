using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    [Obsolete("Use Resources part!")]
    public class Hitpoints : Part
    {
        [SerializeField] private float baseHp;
        [SerializeField] private float baseHpMax;

        public float BaseHpMax
        {
            get => baseHpMax;
            set => baseHpMax = value;
        }

        public override string Name => "Hitpoints";

        protected override void Start()
        {
            base.Start();
            baseHp = GetHpMax();
        }

        public float GetHp()
        {
            GetHpEvent e = LocalEvent.Get<GetHpEvent>();
            e.obj = parent;
            e.hp = baseHp;
            FireEvent(parent, e);
            return e.hp;
        }

        public float GetHpMax()
        {
            GetHpMaxEvent e = LocalEvent.Get<GetHpMaxEvent>();
            e.obj = parent;
            e.hpMax = baseHpMax;
            FireEvent(parent, e);
            return e.hpMax;
        }

        public bool ModifyHp(float amount)
        {
            BeforeHpModifiedEvent beforeHpModifiedEvent = LocalEvent.Get<BeforeHpModifiedEvent>();
            beforeHpModifiedEvent.obj = parent;
            beforeHpModifiedEvent.amount = amount;
            if (!FireEvent(parent, beforeHpModifiedEvent))
                return false;
            amount = beforeHpModifiedEvent.amount;
            baseHp += amount;
            HpModifiedEvent hpModifiedEvent = LocalEvent.Get<HpModifiedEvent>();
            hpModifiedEvent.obj = parent;
            hpModifiedEvent.amount = amount;
            if (GetHp() <= 0)
                parent.Destroy();
            return true;
        }
    }
}
