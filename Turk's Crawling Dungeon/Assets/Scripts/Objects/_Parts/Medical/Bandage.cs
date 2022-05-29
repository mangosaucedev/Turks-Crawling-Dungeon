using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects.Parts.Effects;

namespace TCD.Objects.Parts
{
    public class Bandage : Part
    {
        [SerializeField] private int minBleedEffectsRemoved = 1;
        [SerializeField] private int maxBleedEffectsRemoved = 3;

        public int MinBleedEffectsRemoved
        {
            get => minBleedEffectsRemoved;
            set => minBleedEffectsRemoved = value;  
        }

        public int MaxBleedEffectsRemoved
        {
            get => maxBleedEffectsRemoved;
            set => maxBleedEffectsRemoved = value;
        }

        public override string Name => "Bandage";

        protected override void GetInteractions(GetInteractionsEvent e)
        {
            base.GetInteractions(e);

            BaseObject player = PlayerInfo.currentPlayer;
            Effects.Effects effects = player.Parts.Get<Effects.Effects>();

            if (effects.HasEffect("Bleeding"))
                e.interactions.Add(new Interaction("Apply Bandage", ApplyToSelf));
        }

        private void ApplyToSelf()
        {
            BaseObject player = PlayerInfo.currentPlayer;
            Effects.Effects effects = player.Parts.Get<Effects.Effects>();  
            
            int random = RandomInfo.Random.Next(MinBleedEffectsRemoved, MaxBleedEffectsRemoved);
            bool bandaged = false;
            
            for (int i = 0; i < random; i++)
            {
                if (effects.TryGetEffect(out Bleeding bleeding))
                    bandaged = effects.RemoveEffect(bleeding) || bandaged;
                else
                    break;
            }

            if (bandaged)
                Destroy();
        }

        private void Destroy()
        {
            BaseObject player = PlayerInfo.currentPlayer;
            Inventory inventory = player.Parts.Get<Inventory>();
            if (inventory.TryRemoveItem(parent))
                parent.Destroy();
        }
    }
}
