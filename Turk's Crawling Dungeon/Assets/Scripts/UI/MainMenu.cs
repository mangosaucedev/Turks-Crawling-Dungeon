using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Zones;

namespace TCD.UI
{
    public class MainMenu : ViewController
    {
        protected override string ViewName => gameObject.name;

        public void Continue()
        {
            CloseView();
            ZoneGeneratorManager zoneGeneratorManager = ServiceLocator.Get<ZoneGeneratorManager>();
            zoneGeneratorManager.GenerateZone(ZoneGeneratorType.Generic);
        }
        
        public void NewGame()
        {
            CloseView();
            ZoneGeneratorManager zoneGeneratorManager = ServiceLocator.Get<ZoneGeneratorManager>();
            zoneGeneratorManager.GenerateZone(ZoneGeneratorType.Generic);
        }

        public void Help()
        {
            ViewManager.Open("Help View");
        }

        public void Settings()
        {

        }

        public void ExitToDesktop() => TCDGame.ExitToDesktop();
    }
}
