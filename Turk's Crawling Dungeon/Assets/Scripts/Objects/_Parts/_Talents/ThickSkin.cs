using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Attacks;
using TCD.Objects.Parts;
using TCD.Objects.Parts.Effects;
using TCD.TimeManagement;

namespace TCD.Objects.Parts.Talents
{
    public class ThickSkin : Talent
    {
        public override string Name => "Thick Skin";

        public override string TalentTree => "Fitness";

        public override string IconName => "ThickSkinIcon";

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Passive;

        public override TargetMode TargetMode => TargetMode.None;

        protected override bool CanUseOnObject(BaseObject obj) => true;
        
        protected override void OnObject()
        {

        }

        protected override bool CanUseOnCell(Cell cell) => true;

        protected override void OnCell()
        {

        }

        public override int GetEnergyCost() => 0;

        public override int GetRange(int level) => 1;

        public float GetDamageSoak(int level)
        {
            switch (level)
            {
                default:
                    return 0.03f;
                case 2:
                    return 0.06f;
                case 3:
                    return 0.1f;
                case 4:
                    return 0.13f;
                case 5:
                    return 0.16f;
            }
        }

        public override string GetDescription(int level) => $"Resist {GetDamageSoak(level) * 100}% of all damage taken.";

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == BeforeHpModifiedEvent.id)
                OnBeforeHpModified((BeforeHpModifiedEvent) (LocalEvent) e);
            return base.HandleEvent(e);
        }

        private void OnBeforeHpModified(BeforeHpModifiedEvent e)
        {
            if (e.IsDamage)
            {
                float soak = Mathf.Abs(e.amount * GetDamageSoak(level));
                e.amount = Mathf.Min(0, e.amount + soak);
                DebugLogger.Log($"THICK SKIN: {soak} damage soaked!");
            }
        }
    }
}
