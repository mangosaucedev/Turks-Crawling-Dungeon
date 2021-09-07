using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    [Serializable]
    public class AutoGet : Part
    {
        public override string Name => "Auto Get";

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == EnteredCellEvent.id)
                OnEnteredCell(e);
            return base.HandleEvent(e);
        }

        private void OnEnteredCell(LocalEvent e)
        {
            EnteredCellEvent enteredCellEvent = (EnteredCellEvent) e;
            if (enteredCellEvent.obj == PlayerInfo.currentPlayer &&
                parent.parts.TryGet(out Item item))
                item.PutInPlayerInventory();
        }
    }
}
