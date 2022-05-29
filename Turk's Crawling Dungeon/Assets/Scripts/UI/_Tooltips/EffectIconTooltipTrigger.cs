using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TCD.Objects.Parts;
using TCD.Objects.Parts.Effects;

namespace TCD.UI.Tooltips
{
    public class EffectIconTooltipTrigger : TooltipTrigger
    {
        private static StringBuilder stringBuilder = new StringBuilder();

        [SerializeField] private EffectIcon icon;

        private EffectIcon Icon
        {
            get
            {
                if (!icon)
                    icon = GetComponent<EffectIcon>();
                return icon;
            }
        }

        private Effect Effect
        {
            get
            {
                if (Icon.effects.TryGetEffect(Icon.effectName, out Effect effect))
                    return effect;
                return null;
            }
        }

        private int Stacks => Icon.effects.GetEffectStacks(Icon.effectName);

        private float Time => ((float) Icon.effects.GetMaxTimeRemainingOfEffect(Icon.effectName) / TimeInfo.TIME_PER_STANDARD_TURN).RoundToDecimal(1);

        public override string GetHeader()
        {
            return $"<color=yellow>{Effect.Name}</color>";
        }

        public override string GetBody()
        {
            stringBuilder.Clear();
            stringBuilder.Append($"<color=#039be5>Turns Remaining:</color> {Time}");
            if (Stacks > 1)
                stringBuilder.Append($"\n<color=#039be5>Stacks:</color> {Stacks}");
            stringBuilder.Append($"\n{Effect.GetDescription()}");
            return stringBuilder.ToString();
        }
    }
}
