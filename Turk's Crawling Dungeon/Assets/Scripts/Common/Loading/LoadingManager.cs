using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD
{
    public class LoadingManager : MonoBehaviour
    {
        private const float SECONDS_BEFORE_CLOSE_VIEW = 0.2f;

        public Queue<ILoadingOperation> loadingOperations = new Queue<ILoadingOperation>();

        private float pausedForSeconds;
        private bool isComplete;

#if UNITY_EDITOR
        [SerializeField] private string currentLoadingOperation;
#endif

        public bool IsLoading => loadingOperations.Count > 0;

        public ILoadingOperation CurrentLoadingOperation
        {
            get
            {
                if (!IsLoading)
                    return null;
                return loadingOperations.Peek();
            }
        }

        private TCDGame Game => ServiceLocator.Get<TCDGame>();

        private void Update()
        {
            UpdateLoadingOperations();
            UpdatePauseBeforeClose();
#if UNITY_EDITOR
            if (IsLoading)
                currentLoadingOperation = CurrentLoadingOperation.Name;
            else
                currentLoadingOperation = "NONE";
#endif
        }

        private void UpdateLoadingOperations()
        {
            while (IsLoading)
            {
                if (!CurrentLoadingOperation.HasStarted)
                {
                    if (Game.State != "Loading")
                        Game.State = "Loading";
                    StartCoroutine(CurrentLoadingOperation.Load());
                    isComplete = false;
                    CurrentLoadingOperation.HasStarted = true;
                }
                if (CurrentLoadingOperation.IsDone)
                    FinishCurrentLoadingOperation();
                else
                    break;
            }
        }

        public void EnqueueLoadingOperation(ILoadingOperation loadingOperation)
        {
            loadingOperations.Enqueue(loadingOperation);
        }

        public IEnumerator EnqueueLoadingOperationRoutine(ILoadingOperation loadingOperation)
        {
            EnqueueLoadingOperation(loadingOperation);
            yield return new WaitUntil(() => { return loadingOperation.IsDone; });
        }

        public void FinishCurrentLoadingOperation()
        {
            loadingOperations.Dequeue();
            if (!IsLoading)
            {
                isComplete = true;
                pausedForSeconds = SECONDS_BEFORE_CLOSE_VIEW;
            }
        }

        private void UpdatePauseBeforeClose()
        {
            if (!isComplete)
                return;
            if (pausedForSeconds > 0f)
                pausedForSeconds -= Time.deltaTime;
            else
            {
                isComplete = false;
                Game.State = Game.desiredState;
            }
        }
    }
}
