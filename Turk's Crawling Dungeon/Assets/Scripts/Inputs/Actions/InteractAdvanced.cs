using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.UI;

namespace TCD.Inputs.Actions
{
    public class InteractAdvanced : PlayerAction
    {
        public override string Name => "Interact Advanced";

        public override MovementMode MovementMode => MovementMode.Point;

        public override int GetRange() => 1;

        public override IEnumerator OnObject(BaseObject target)
        {
            if (target == null)
                yield break;
            SelectionHandler.SetSelectedObject(target);
            ViewManager.Open("Interaction List");
        }

        public override IEnumerator OnCell(Cell cell)
        {
            if (cell.objects.Count > 1)
            {
                SelectionHandler.SetSelectedCell(cell);
                ViewManager.Open("Interaction Object List");
            }
            else if (cell.objects.Count == 1)
                yield return OnObject(cell.objects[0]);
        }
    }
}
