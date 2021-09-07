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

        public override Sprite Icon => Assets.Get<Sprite>("ThickSkinIcon");

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Passive;

        public override TargetMode TargetMode => TargetMode.None;

        public override IEnumerator OnObjectRoutine(BaseObject obj)
        {
            yield break;
        }

        public override IEnumerator OnCellRoutine(Cell cell)
        {
            yield break;
        }

        public override int GetEnergyCost() => 0;

        public override int GetRange() => 1;

        public float GetDamageSoak()
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

        public override string GetDescription() => $"Resist {GetDamageSoak() * 100}% of all damage taken.";
    }
}
