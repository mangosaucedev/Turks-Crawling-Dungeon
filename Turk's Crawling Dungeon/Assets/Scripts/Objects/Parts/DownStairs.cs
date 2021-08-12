using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.UI;

namespace TCD.Objects.Parts
{
    public class DownStairs : Part
    {
        public override string Name => "Down Stairs";

        public override bool HandleEvent<T>(T e)
        {
            if (e.Id == EnteredCellEvent.id)
                OnEnteredCell(e);
            return base.HandleEvent(e);
        }

        private void OnEnteredCell(LocalEvent e)
        {
            EnteredCellEvent enteredCellEvent = (EnteredCellEvent) e;
            if (enteredCellEvent.obj == PlayerInfo.currentPlayer)
                ViewManager.Open("Down Stairs View");  
        }
    }
}
