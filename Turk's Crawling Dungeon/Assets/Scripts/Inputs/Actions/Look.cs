using System.Collections;
using System.Collections.Generic;
using TCD.Graphics.Indicators;
using TCD.Objects;
using TCD.Objects.Parts;
using TCD.UI;
using UnityEngine;

namespace TCD.Inputs.Actions
{
    public class Look : PlayerAction
    {
        public override string Name => "Look";

        public override MovementMode MovementMode => MovementMode.Cursor;

        public override int GetRange() => 12;

        public override void Start()
        {
            base.Start();
            IndicatorHandler.ShowIndicator("Player To Cursor Indicator", GetRange(), false);
        }

        public override void End()
        {
            base.End();
            IndicatorHandler.HideIndicator();
        }

        public override IEnumerator OnCell(Cell cell)
        {
            SelectionHandler.SetSelectedCell(cell);
            if (cell.Objects.Count == 1)
                yield return OnObject(cell.Objects[0]);
            else if (cell.Objects.Count > 1)
                ViewManager.Open("Inspect Object List");
        }

        public override IEnumerator OnObject(BaseObject target)
        {
            SelectionHandler.SetSelectedObject(target);
            ViewManager.Open("Inspect View");
            yield break;
        }
    }
}
