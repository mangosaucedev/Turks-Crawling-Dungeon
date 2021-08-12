using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public abstract class Liquid : Part
    {
        [SerializeField] protected string liquidColor;

        public string LiquidColor
        {
            get => liquidColor;
            set => liquidColor = value;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Render render = parent.parts.Get<Render>();
            render.AddColorLayer(Name, GetColor());
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Render render = parent.parts.Get<Render>();
            render.RemoveColorLayer(Name);
        }

        protected Color GetColor()
        {
            if (!ColorUtility.TryParseHtmlString(LiquidColor, out Color color))
                color = Color.white;
            return color;
        }

        protected override void GetInteractions(GetInteractionsEvent e)
        {
            base.GetInteractions(e);
            e.interactions.Add(new Interaction("Drink", OnDrink));
        }

        private void OnDrink()
        {
            LiquidEffects.Drink(Name, PlayerInfo.currentPlayer);
        }
    }
}
