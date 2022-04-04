using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Parts.Effects;

namespace TCD.UI
{
    public class EffectsView : MonoBehaviour
    {
        private List<EffectIcon> effectIcons = new List<EffectIcon>();
        private HashSet<string> builtEffects = new HashSet<string>();

        private void OnEnable()
        {
            EventManager.Listen<AfterTurnTickEvent>(this, OnAfterTurnTick);
        }

        private void OnDisable()
        {
            EventManager.StopListening<AfterTurnTickEvent>(this);
        }

        private void OnAfterTurnTick(AfterTurnTickEvent e) => UpdateEffectIcons();
        

        private void UpdateEffectIcons()
        {
            builtEffects.Clear();
            ClearEffectIcons();
            BuildEffectIcons();
        }

        private void ClearEffectIcons()
        {
            for (int i = effectIcons.Count - 1; i >= 0; i--)
            {
                EffectIcon effectIcon = effectIcons[i];
                if (effectIcon)
                    Destroy(effectIcon.gameObject);
            }
            effectIcons.Clear();
        }

        private void BuildEffectIcons()
        {
            BaseObject player = PlayerInfo.currentPlayer;
            Effects effects = player.Parts.Get<Effects>();
            foreach (Effect effect in effects.activeEffects)
            {
                if (!builtEffects.Contains(effect.Name))
                {
                    BuildEffectIcon(effect.Name);
                    builtEffects.Add(effect.Name);
                }
            }
        }

        private void BuildEffectIcon(string effectName)
        {
            GameObject prefab = Assets.Get<GameObject>("Effect Icon");
            GameObject gameObject = Instantiate(prefab, transform);
            EffectIcon effectIcon = gameObject.GetComponent<EffectIcon>();
            effectIcon.effectName = effectName;
            effectIcon.effects = PlayerInfo.currentPlayer.Parts.Get<Effects>();
            effectIcons.Add(effectIcon);
        }
    }
}
