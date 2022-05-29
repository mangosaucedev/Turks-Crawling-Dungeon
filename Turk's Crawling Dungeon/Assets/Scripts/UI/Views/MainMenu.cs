using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RedBlueGames.Tools.TextTyper;
using TCD.Zones;
using TCD.Zones.Dungeons;

namespace TCD.UI
{
    public class MainMenu : ViewController
    {
        [Header("Cache")]
        [SerializeField] private GameObject developerToolsButton;
        [SerializeField] private TextTyper titleTyper;

        protected override string ViewName => gameObject.name;

        private void Start()
        {
            titleTyper.TypeText("TURK'S\nCRAWLING\nDUNGEON", 0.08f);
            //if ((bool) GlobalVars.Get("bool_DeveloperMode"))
            //    developerToolsButton.SetActive(true);
        }

        public void Continue()
        {
            CloseView();
            TCDGame.StartNewGame();
        }
        
        public void NewGame()
        {
            //CloseView();
            //Embark.SetChosenClass(Assets.Get<TCD.Objects.Parts.Class>("Turkinopoulos"));
            //TCDGame.StartNewGame();
            ViewManager.Open("Prerelease Embark View");
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


        public void OnPrintCompleted()
        {

        }

        public void OnCharacterPrinted()
        {

        }
    }
}
