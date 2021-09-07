using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using TCD.Inputs;
using TCD.Objects;
using TCD.Objects.Parts;
using TCD.UI.Tooltips;

namespace TCD
{
    public class ObjectTooltipManager : MonoBehaviour
    {
        private const int TICKS_BEFORE_DISPLAY = 5;

        private Vector2Int currentPosition;
        private bool isShowingTooltip;
        private int ticksBeforeDisplay;
        private StringBuilder stringBuilder = new StringBuilder();
        private WaitForSeconds wait = new WaitForSeconds(0.1f);

        private void OnEnable()
        {
            StartCoroutine(CheckRoutine());   
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator CheckRoutine()
        {
            while (true)
            {
                CheckIfMouseOverObject();
                yield return wait;
            }
        }

        private void CheckIfMouseOverObject()
        {
            if (EventSystem.current.IsPointerOverGameObject() || !InputManager.IsActive)
            {
                if (isShowingTooltip)
                    HideTooltip();
            }
            else
            {
                MouseCursor mouseCursor = ServiceLocator.Get<MouseCursor>();
                Vector2Int position = mouseCursor.GetGridPosition();
                Vector2Int previousPosition = currentPosition;
                currentPosition = position;
                if (previousPosition != position)
                    HideTooltip();
                if (ticksBeforeDisplay > 0)
                    ticksBeforeDisplay--;
                else
                    UpdateTooltip();
            }
        }

        private void UpdateTooltip()
        {
            GameGrid grid = CurrentZoneInfo.grid;
            if (grid == null || !grid.IsWithinBounds(currentPosition))
            {
                if (isShowingTooltip)
                    HideTooltip();
                return;
            }
            Cell cell = grid[currentPosition];
            List<BaseObject> visibleObjects = cell.GetVisibleObjects(); 
            stringBuilder.Clear();
            for (int i = 0; i < visibleObjects.Count; i++)
            {
                BaseObject obj = visibleObjects[i];
                if (i > 0)
                    stringBuilder.Append("\n---------\n");
                AddDescriptionOfObjectToTooltip(obj);
            }
            if (stringBuilder.Length > 0)
            {
                if (isShowingTooltip)
                    HideTooltip();
                ShowTooltip();
            }
            else if (isShowingTooltip)
                HideTooltip();
        }


        private void AddDescriptionOfObjectToTooltip(BaseObject obj)
        {
            string displayName = obj.GetDisplayName();
            string description = obj.GetDescription();
            stringBuilder.Append($"<color=yellow>{displayName}</color>");
            stringBuilder.Append($"\n{description}");
        }

        private void HideTooltip()
        {
            ticksBeforeDisplay = TICKS_BEFORE_DISPLAY;
            if (isShowingTooltip)
            {
                TooltipHandler.Hide();
                isShowingTooltip = false;
            }
        }

        private void ShowTooltip()
        {
            TooltipHandler.Show("Current cell:", stringBuilder.ToString());
            isShowingTooltip = true;
        }
    }
}
