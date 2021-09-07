using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Objects.Parts;
using TCD.UI;

namespace TCD.Inputs.Actions
{
    public class Interact : PlayerAction
    {
        public override string Name => "Interact";

        public override MovementMode MovementMode => MovementMode.Point;

        public override int GetRange() => 1;

        public override IEnumerator OnObject(BaseObject target)
        {
            if (target == null)
                yield break;
            GetInteractionsEvent e = LocalEvent.Get<GetInteractionsEvent>();
            e.obj = target;
            target.HandleEvent(e);
            if (e.interactions.Count == 0)
                yield break;
            if (e.interactions.Count == 1)
            {
                e.interactions[0].onInteract();
                yield break;
            }
            foreach (Interaction interaction in e.interactions)
            {
                if (interaction.isDefault)
                {
                    interaction.onInteract();
                    yield break;
                }
            }
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
