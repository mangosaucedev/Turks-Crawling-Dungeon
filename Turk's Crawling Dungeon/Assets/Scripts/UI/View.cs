using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Inputs;

namespace TCD.UI
{
    public class View : MonoBehaviour
    {
        public event Action SetActiveEvent = delegate { };
        public event Action SetInactiveEvent = delegate { };
        public event Action UpdateEvent = delegate { };
        public event Action CloseEvent = delegate { };
        public bool isActive;
        [SerializeField] private bool isCancellable;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private bool refreshOnStart;
        [SerializeField] private bool refreshOnUpdate;
        

        private string ViewName => gameObject.name;

        private void Start()
        {
            if (refreshOnStart)
                Refresh();
        }

        private void Update()
        {
            UpdateIsViewActive();
        }

        private void OnEnable()
        {
            SetActiveEvent += UpdateEvent.Invoke;
            if (refreshOnUpdate)
                UpdateEvent += Refresh;
            EventManager.Listen<ViewOpenedEvent>(this, OnViewOpened);
            EventManager.Listen<BeforeViewClosedEvent>(this, OnBeforeViewClosed);
            EventManager.Listen<ViewClosedEvent>(this, OnViewClosed);
            EventManager.Listen<KeyEvent>(this, OnKey);
        }

        private void OnDisable()
        {
            SetActiveEvent -= UpdateEvent.Invoke;
            if (refreshOnUpdate)
                UpdateEvent -= Refresh;            
            EventManager.StopListening<ViewOpenedEvent>(this);
            EventManager.StopListening<BeforeViewClosedEvent>(this);
            EventManager.StopListening<ViewClosedEvent>(this);
            EventManager.StopListening<KeyEvent>(this);
        }

        public void UpdateIsViewActive() => SetActive(ViewManager.GetActiveViewName() == ViewName);
        
        public void SetActive(bool value)
        {
            if (value)
            {
                if (!isActive)
                    SetActiveEvent?.Invoke();
            }
            else
            {
                if (isActive)
                    SetInactiveEvent?.Invoke();
            }
            isActive = value;
        }

        private void OnViewOpened(ViewOpenedEvent e) => UpdateEvent?.Invoke();

        private void OnBeforeViewClosed(BeforeViewClosedEvent e)
        {
            if (e.view.name == ViewName)
                CloseEvent?.Invoke();
        }

        private void OnViewClosed(ViewClosedEvent e) => UpdateEvent?.Invoke();

        private void OnKey(KeyEvent e)
        {
            KeyEventContext context = e.context;
            KeyCommand command = context.command;
            KeyState state = context.state;
            if (isCancellable && isActive && command == KeyCommand.Cancel && state == KeyState.PressedThisFrame)
                ViewManager.Close(ViewName);
        }

        private void Refresh()
        {
            if (!canvasGroup)
                return;
            StopAllCoroutines();
            StartCoroutine(RefreshRoutine());
        }

        private IEnumerator RefreshRoutine()
        {
            Canvas.ForceUpdateCanvases();
            canvasGroup.alpha = 0;
            yield return null;
            canvasGroup.alpha = 1;
        }
    }
}
