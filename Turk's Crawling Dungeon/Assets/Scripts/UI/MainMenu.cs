using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones;
using TCD.Zones.Dungeons;

namespace TCD.UI
{
    public class MainMenu : ViewController
    {
        [SerializeField] private GameObject developerToolsButton;

        protected override string ViewName => gameObject.name;

        private void Start()
        {
            if ((bool) GlobalVars.Get("bool_DeveloperMode"))
                developerToolsButton.SetActive(true);
        }

        public void Continue()
        {
            CloseView();
            TCDGame.StartNewGame();
        }
        
        public void NewGame()
        {
            ViewManager.Open("Embark View");
        }

        public void Help()
        {
            ViewManager.Open("Help View");
        }

        public void Settings()
        {
            ViewManager.Open("Settings View");
        }

        public void ExitToDesktop() => TCDGame.ExitToDesktop();
    }
}
