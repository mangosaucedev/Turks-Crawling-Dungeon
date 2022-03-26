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
    [PlayerTalent("Lifeblood"), Serializable]
    public class Lifeblood : Talent
    {
        public override string Name => "Lifeblood";

        public override string TalentTree => "Fitness";

        public override Sprite Icon => Assets.Get<Sprite>("LifebloodIcon");

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

        public float GetHealingMultiplier()
        {
            switch (level)
            {
                default:
                    return 10f;
                case 2:
                    return 12.5f;
                case 3:
                    return 15f;
                case 4:
                    return 17.5f;
                case 5:
                    return 20f;
            }
        }

        public int GetDuration()
        {
            switch (level)
            {
                default:
                    return 5 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 2:
                    return 5 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 3:
                    return 6 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 4:
                    return 6 * TimeInfo.TIME_PER_STANDARD_TURN;
                case 5:
                    return 7 * TimeInfo.TIME_PER_STANDARD_TURN;
            }
        }

        public override string GetDescription() => $"On reaching below 30% health, your health regeneration will " +
            $"increase {GetHealingMultiplier() * 100}% for {((float) GetDuration() / TimeInfo.TIME_PER_STANDARD_TURN).RoundToDecimal(1)} " +
            $"turns.";
    }
}
