using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.UI.Tooltips
{
    public static class TooltipHandler
    {
        public static bool canShowTooltip;

        private static Tooltip currentTooltip;
        private static bool playerMovedLastFrame;

        static TooltipHandler()
        {
            EventManager.Listen<PlayerMovedEvent>(new GenericEventListener(), OnPlayerMoved);
            MouseCursor cursor = ServiceLocator.Get<MouseCursor>();
            cursor.onMouseMoved += OnMouseMoved;
        }

        private static void OnPlayerMoved(PlayerMovedEvent e)
        {
            Hide();
            canShowTooltip = false;
            playerMovedLastFrame = true;
        }

        private static void OnMouseMoved()
        {
            if (playerMovedLastFrame)
            {
                playerMovedLastFrame = false;
                return;
            }
            canShowTooltip = true;
        }

        public static void Show(string header, string body)
        {
            if (currentTooltip)
                Hide();
            if (!canShowTooltip)
                return;
            GameObject prefab = Assets.Get<GameObject>("Tooltip");
            GameObject gameObject = Object.Instantiate(prefab, ParentManager.Canvas);
            currentTooltip = gameObject.GetComponent<Tooltip>();
            currentTooltip.header.text = header;
            currentTooltip.body.text = body;
        }

        public static void Hide()
        {
            if (currentTooltip)
                Object.Destroy(currentTooltip.gameObject);
            currentTooltip = null;
        }
    }
}
