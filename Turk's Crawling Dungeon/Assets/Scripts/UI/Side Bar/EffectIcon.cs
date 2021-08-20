using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects;
using TCD.Objects.Parts.Effects;

namespace TCD.UI
{
    public class EffectIcon : MonoBehaviour
    {
        public Effects effects;
        public string effectName;

        [SerializeField] private Image icon;
        [SerializeField] private Text stacks;
        [SerializeField] private Text timeRemaining;

        private void Start()
        {
            UpdateIcon();
        }

        public void UpdateIcon()
        {
            if (!effects.TryGetEffect(effectName, out Effect effect))
                return;
            icon.sprite = effect.Icon;
            int stackCount = effects.GetEffectStacks(effectName);
            stacks.text = stackCount == 0 ? "" : stackCount.ToString();
            float time = effects.GetMaxTimeRemainingOfEffect(effectName);
            time /= TimeInfo.TIME_PER_STANDARD_TURN;
            time = time.RoundToDecimal(1);
            timeRemaining.text = time.ToString();
        }
    }
}
