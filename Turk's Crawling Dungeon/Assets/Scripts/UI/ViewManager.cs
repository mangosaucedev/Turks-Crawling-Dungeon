using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Inputs;

namespace TCD.UI
{
    public class ViewManager : MonoBehaviour
    {
        private static List<ActiveView> activeViews = new List<ActiveView>();

        public static string GetActiveView()
        {
            string viewName = "Null";
            if (activeViews.Count > 0)
            {
                ActiveView activeView = activeViews[0];
                foreach (ActiveView view in activeViews)
                {
                    if (view.isInteractive && (view.locksInput || !activeView.locksInput))
                        activeView = view;
                }
                viewName = activeView.name;
            }
            return viewName;
        }

        public static void Open(string viewName, bool locksInput = true, bool isInteractive = true)
        {
            if (TryFind(viewName, out ActiveView view))
                Close(viewName);
            view = new ActiveView(viewName, locksInput, isInteractive);
            GameObject prefab = Assets.Get<GameObject>(viewName);
            Transform parent = ParentManager.Canvas;
            view.gameObject = Instantiate(prefab, parent);
            view.gameObject.name = view.gameObject.name.Replace("(Clone)", "");
            activeViews.Add(view);
            EventManager.Send(new ViewOpenedEvent(view));
        }

        private static bool TryFind(string viewName, out ActiveView foundView)
        {
            foundView = default;
            foreach (ActiveView view in activeViews)
            {
                if (view.name == viewName)
                {
                    foundView = view;
                    return true;
                }
            }
            return false;
        }

        public static void Close(string viewName)
        {
            if (!TryFind(viewName, out ActiveView view))
                return;
            activeViews.Remove(view);
            Destroy(view.gameObject);
            EventManager.Send(new ViewClosedEvent(view));
        }

        public static void CloseAll()
        {
            for (int i = activeViews.Count - 1; i >= 0; i--)
            { 
                ActiveView view = activeViews[i];
                string viewName = view.name;
                Close(viewName);
            }
        }

        public static IEnumerator OpenAndWaitForViewRoutine(string viewName)
        {
            Open(viewName);
            while (TryFind(viewName, out ActiveView activeView))
                yield return null;
        }
    }
}
