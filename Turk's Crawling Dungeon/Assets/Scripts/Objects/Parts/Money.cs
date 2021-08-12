using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class Money : Part
    {
        public override string Name => "Money";

        [SerializeField] private float baseValue;
        [SerializeField] private float minStartingValue;
        [SerializeField] private float maxStartingValue;

        public float BaseValue
        {
            get => baseValue;
            set => baseValue = value;
        }

        public float MinStartingValue
        {
            get => minStartingValue;
            set => minStartingValue = value;
        }

        public float MaxStartingValue
        {
            get => maxStartingValue;
            set => maxStartingValue = value;
        }

        protected override void Start()
        {
            base.Start();
            if (MinStartingValue > 0 && MaxStartingValue > 0)
                BaseValue = Random.Range(MinStartingValue, MaxStartingValue);
        }

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == PickedUpEvent.id)
                PutInWallet();
            return base.HandleEvent(e);
        }

        private void PutInWallet()
        {
            Item item = parent.parts.Get<Item>();
            BaseObject obj = item.Owner;
            if (obj.parts.TryGet(out Wallet wallet))
            {
                wallet.AddMoney((decimal) BaseValue.RoundToDecimal(2));
                parent.Destroy();
            }
        }
    }
}
