using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class LootGraphic : Graphic
    {
        private bool isEnabled;

        public override string Name => "Loot Graphic";

        protected override Sprite Sprite => GetSprite();

        protected override void OnEnable()
        {
            base.OnEnable();
            EventManager.Listen<AfterTurnTickEvent>(this, OnAfterTurnTick);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EventManager.StopListening<AfterTurnTickEvent>(this);
        }

        private void OnAfterTurnTick(AfterTurnTickEvent e)
        {
            int distanceToPlayer = PlayerInfo.GetDistanceToPlayer(parent);
            if (distanceToPlayer == 1)
                Enable();
            else
                Disable();
        }

        private void Enable()
        {
            if (graphicRenderer == null)
                return;
            if (isEnabled)
                return;
            isEnabled = true;
            graphicRenderer.sprite = GetSprite();
        }

        private void Disable()
        {
            if (graphicRenderer == null)
                return;
            if (!isEnabled)
                return;
            isEnabled = false;
            graphicRenderer.sprite = null;
        }

        private Sprite GetSprite()
        {
            if (!isEnabled || !parent.Parts.TryGet(out Inventory inventory) || inventory.items.Count == 0)
                return null;
            return Assets.Get<Sprite>(inventory.items[0].Parts.Get<Render>().Sprite);
        }
    }
}
