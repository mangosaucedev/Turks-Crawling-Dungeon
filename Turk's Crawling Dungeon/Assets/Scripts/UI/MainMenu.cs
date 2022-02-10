using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones;
using TCD.Zones.Dungeons;

namespace TCD.UI
{
    public class MainMenu : ViewController
    {
        protected override string ViewName => gameObject.name;

        public void Continue()
        {
            CloseView();
            TCDGame.StartNewGame();
        }
        
        public void NewGame()
        {
            CloseView();
            TCDGame.StartNewGame();
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
