using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones;

namespace TCD.UI
{
    public class DownStairsView : ViewController
    {
        [SerializeField] private ViewButton continueButton;
        [SerializeField] private ViewButton cancelButton;

        protected override string ViewName => gameObject.name;

        protected override void Awake()
        {
            base.Awake();
            continueButton.onClick.AddListener(OnContinue);
            cancelButton.onClick.AddListener(CloseView);
        }

        private void OnContinue()
        {
            CloseView();
            ScoreHandler.level++;
            ScoreHandler.ModifyScore(1000);
            ZoneResetter.ResetZone();
        }
    }
}
