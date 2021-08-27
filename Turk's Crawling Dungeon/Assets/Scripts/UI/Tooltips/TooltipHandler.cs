using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.UI.Tooltips
{
    public static class TooltipHandler
    {
        private static Tooltip currentTooltip;

        public static void Show(string header, string body)
        {
            if (currentTooltip)
                Hide();
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
