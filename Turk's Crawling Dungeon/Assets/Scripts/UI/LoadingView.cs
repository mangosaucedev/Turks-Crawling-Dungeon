using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TCD.UI
{
    public class LoadingView : MonoBehaviour
    {
        [SerializeField] private Text message;
        [SerializeField] private Image bar;

        private LoadingManager loadingManager;

        private void Awake()
        {
            loadingManager = ServiceLocator.Get<LoadingManager>();
        }

        private void Update()
        {
            ILoadingOperation operation = loadingManager.CurrentLoadingOperation;
            if (operation != null)
            {
                message.text = operation.Message;
                bar.fillAmount = operation.Progress;
            }
            else
                message.text = "...";
        }
    }
}
