using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TCD.Objects.Parts;
using TCD.Objects.Parts.Talents;

namespace TCD.UI.Tooltips
{
    public class HotbarButtonTooltipTrigger : TooltipTrigger
    {
        private static StringBuilder stringBuilder = new StringBuilder();

        [SerializeField] private HotbarButton button;

        private HotbarButton HotbarButton
        {
            get
            {
                if (!button)
                    button = GetComponent<HotbarButton>();
                return button;
            }
        }

        private Talent Talent => HotbarButton.talent;

        public override string GetHeader()
        {
            return $"<color=yellow>{Talent.Name}</color>";
        }

        public override string GetBody()
        {
            stringBuilder.Clear();
            stringBuilder.Append($"<color=#039be5>Level:</color> {Talent.level}");
            switch (Talent.UseMode)
            {
                case UseMode.Activated:
                    stringBuilder.Append("\n<color=#039be5>Use Mode:</color> Activated");
                    break;
                case UseMode.Toggle:
                    stringBuilder.Append("\n<color=#039be5>Use Mode:</color> Toggle");
                    break;
                default:
                    stringBuilder.Append("\n<color=#039be5>Use Mode:</color> Passive");
                    break;
            }
            switch (Talent.TargetMode)
            {
                case TargetMode.Attack:
                    stringBuilder.Append("\n<color=#039be5>Target Mode:</color> Attack");
                    break;
                case TargetMode.Cell:
                    stringBuilder.Append("\n<color=#039be5>Target Mode:</color> Cell");
                    break;
                case TargetMode.Object:
                    stringBuilder.Append("\n<color=#039be5>Target Mode:</color> Object");
                    break;
                default:
                    break;
            }
            Resource resource = Talent.Resource;
            float activationResourceCost = Talent.GetActivationResourceCost();
            if (activationResourceCost > 0)
                stringBuilder.Append($"\n<color=#039be5>Activation Cost:</color> {activationResourceCost} {resource}");
            float sustainResourceCost = Talent.GetSustainResourceCost();
            if (sustainResourceCost > 0)
                stringBuilder.Append($"\n<color=#039be5>Sustain Cost:</color> {sustainResourceCost} {resource}");
            int range = Talent.GetRange();
            if (range <= 1)
                stringBuilder.Append($"\n<color=#039be5>Range:</color> Melee");
            else
                stringBuilder.Append($"\n<color=#039be5>Range:</color> {range}");
            float turnsToUse = ((float) Talent.GetEnergyCost() / TimeInfo.TIME_PER_STANDARD_TURN).RoundToDecimal(1);
            if (turnsToUse == 0)
                stringBuilder.Append($"\n<color=#039be5>Turns To Use:</color> Instant (0)");
            else
                stringBuilder.Append($"\n<color=#039be5>Turns To Use:</color> {turnsToUse}");
            float cooldown = ((float) Talent.GetCooldown() / TimeInfo.TIME_PER_STANDARD_TURN).RoundToDecimal(1);
            stringBuilder.Append($"\n<color=#039be5>Cooldown:</color> {cooldown}");
            stringBuilder.Append($"\n{Talent.GetDescription()}");
            return stringBuilder.ToString();
        }
    }
}
