using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources = TCD.Objects.Parts.Resources;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class HpPotion : Part
    {
        public override string Name => "Hp Potion";

        [SerializeField] private float heal;

        public float Heal
        {
            get => heal;
            set => heal = value;
        }

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == PickedUpEvent.id)
                OnPickedUp();
            return base.HandleEvent(e);
        }

        private void OnPickedUp()
        {
            Item item = parent.Parts.Get<Item>();
            BaseObject obj = item.Owner;
            if (item.inventory.TryRemoveItem(parent) && obj.Parts.TryGet(out Resources resources))
            {
                resources.ModifyResource(Resource.Hitpoints, Heal);
                if (obj == PlayerInfo.currentPlayer)
                    MessageLog.Add("You feel refreshed and full of life.");
                parent.Destroy();
            }
        }
    }
}
