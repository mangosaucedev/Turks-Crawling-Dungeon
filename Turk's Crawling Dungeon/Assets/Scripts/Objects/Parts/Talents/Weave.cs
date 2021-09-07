using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts.Effects;
using TCD.TimeManagement;

namespace TCD.Objects.Parts.Talents
{
    [Serializable]
    public class Weave : Talent
    {
        public override string Name => "Weave";

        public override string TalentTree => "BasicAtheltics";

        public override Sprite Icon => Assets.Get<Sprite>("WeaveIcon");

        public override int MaxLevel => 5;

        public override UseMode UseMode => UseMode.Activated;

        public override TargetMode TargetMode => TargetMode.None;

        public override int GetActivationResourceCost() => 14;

        public override IEnumerator OnObjectRoutine(BaseObject obj)
        {
            DuckAndWeave();
            yield break;
        }

        public override IEnumerator OnCellRoutine(Cell cell)
        {
            DuckAndWeave();
            yield break;
        }

        private void DuckAndWeave()
        {

        }

        public override int GetEnergyCost() => 0;

        public override int GetRange() => 1;

        public override string GetDescription() => $"You duck and weave around enemy " +
            $"attacks, granting you {GetEvasion()}% evasion for {((float) GetDuration() / TimeInfo.TIME_PER_STANDARD_TURN).RoundToDecimal(1)} " +
            $"turns.";

        public int GetEvasion()
        {
            switch (level)
            {
                default:
                    return 18;
                case 2:
                    return 24;
                case 3:
                    return 30;
                case 4:
                    return 34;
                case 5:
                    return 38;
            }
        }

        public float GetDuration()
        {
            switch (level)
            {
                default:
                    return 3;
                case 2:
                    return 4;
                case 3:
                    return 4;
                case 4:
                    return 5;
                case 5:
                    return 6;
            }
        }
    }
}
