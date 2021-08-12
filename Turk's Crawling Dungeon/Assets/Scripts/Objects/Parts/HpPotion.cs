using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
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
            Item item = parent.parts.Get<Item>();
            BaseObject obj = item.Owner;
            if (obj.parts.TryGet(out Hitpoints hitpoints))
            {
                hitpoints.ModifyHp(Heal);
                if (obj == PlayerInfo.currentPlayer)
                    MessageLog.Add("You feel refreshed and full of life.");
                parent.Destroy();
            }
        }
    }
}
