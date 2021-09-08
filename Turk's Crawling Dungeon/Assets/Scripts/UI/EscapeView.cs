using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones;

namespace TCD.UI
{
    public class EscapeView : ViewController
    {
        [SerializeField] private ViewButton continueButton;
        [SerializeField] private ViewButton helpButton;
        [SerializeField] private ViewButton restartButton;
        [SerializeField] private ViewButton exitToMainMenuButton;
        [SerializeField] private ViewButton exitToDesktopButton;

        protected override string ViewName => gameObject.name;

        private void Start()
        {
            continueButton.onClick.AddListener(CloseView);
            helpButton.onClick.AddListener(Help);
            restartButton.onClick.AddListener(Restart);
            exitToMainMenuButton.onClick.AddListener(ExitToMainMenu);
            exitToDesktopButton.onClick.AddListener(ExitToDesktop);
        }

        private void Help()
        {
            CloseView();
            ViewManager.Open("Help View");
        }

        private void Restart()
        {
            CloseView();
            GameResetter.ResetGame();
        }

        private void ExitToMainMenu()
        {
            CloseView();
            ViewManager.Open("Main Menu");
            ZoneResetter zoneResetter = ServiceLocator.Get<ZoneResetter>();
            zoneResetter.UnloadZone(true);
        }


        private void ExitToDesktop() => TCDGame.ExitToDesktop();
    }
}
