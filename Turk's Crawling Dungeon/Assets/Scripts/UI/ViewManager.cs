using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TCD.Inputs;

namespace TCD.UI
{
    public class ViewManager : MonoBehaviour
    {
        public static List<ActiveView> activeViews = new List<ActiveView>();

#if UNITY_EDITOR
        [SerializeField] private string currentView;
#endif

        private Coroutine selectTopLeftSelectableCoroutine;

        private void OnEnable()
        {
            EventManager.Listen<ViewOpenedEvent>(this, OnViewOpened);
            EventManager.Listen<ViewClosedEvent>(this, OnViewClosed);
        }

        private void OnDisable()
        {
            EventManager.StopListening<ViewOpenedEvent>(this);
            EventManager.StopListening<ViewClosedEvent>(this);
        }

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
#if UNITY_EDITOR
            ServiceLocator.Get<ViewManager>().currentView = viewName;
#endif
            return viewName;
        }

        private void OnViewOpened(ViewOpenedEvent e) => SelectTopLeftSelectable();

        private void OnViewClosed(ViewClosedEvent e) => SelectTopLeftSelectable();

        public void SelectTopLeftSelectable()
        {
            this.EnsureCoroutineStopped(ref selectTopLeftSelectableCoroutine);
            selectTopLeftSelectableCoroutine = StartCoroutine(SelectTopLeftSelectableRoutine());
        }

        private IEnumerator SelectTopLeftSelectableRoutine()
        {
            yield return null;
            yield return null; 
            yield return null;
            Vector3 topLeft = Camera.main.ViewportToScreenPoint(new Vector3(0, 1, 0));
            Selectable[] selectables = FindObjectsOfType<Selectable>();
            float shortestDistanceFromTopLeft = float.MaxValue;
            GameObject selectedObject = null;
            foreach (Selectable selectable in selectables)
            {
                Vector3 selectablePostion = selectable.transform.position;
                float distance = Vector3.Distance(selectablePostion, topLeft);
                if (selectable.interactable && distance < shortestDistanceFromTopLeft)
                {
                    selectedObject = selectable.gameObject;
                    shortestDistanceFromTopLeft = distance;
                }
            }
            EventSystem.current.SetSelectedGameObject(selectedObject);
        }

        public static string GetActiveViewName()
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

        public static bool TryGetActiveView(out ActiveView activeView)
        {
            if (TryFind(GetActiveViewName(), out activeView))
                return true;
            return false;
        }

        public static bool ActiveViewLocksInput()
        {
            if (!TryGetActiveView(out ActiveView view))
                return false;
            return view.locksInput;
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

        public static bool IsOpen(string viewName) => TryFind(viewName, out var view);

        public static bool TryFind(string viewName, out ActiveView foundView)
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
            EventManager.Send(new BeforeViewClosedEvent(view));
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

        public static void CloseAllInactive()
        {
            string activeView = GetActiveView();
            for (int i = activeViews.Count - 1; i >= 0; i--)
            {
                ActiveView view = activeViews[i];
                string viewName = view.name;
                if (viewName != activeView)
                    Close(viewName);
            }
        }
    }
}
