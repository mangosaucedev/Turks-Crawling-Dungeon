using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TCD.IO.Serialization;
using TCD.UI;
using TCD.UI.Notifications;
using TCD.Zones.Dungeons;

namespace TCD
{
    public class TCDGame : MonoBehaviour
    { 
        [SerializeField] private string defaultCampaignName;
        [SerializeField] private GameObject startupScreen;

        private IEnumerator Start()
        {
            startupScreen = Instantiate(startupScreen, ParentManager.Canvas);
            yield return GameStartup.StartGame();
            Destroy(startupScreen);
            /*
            yield return ViewManager.OpenAndWaitForViewRoutine("Loading View");
            yield return ViewManager.OpenAndWaitForViewRoutine("Help View");
            NotificationHandler.Notify("Enter the Crawling Dungeon", "It is said that the mind can " +
                "make a hell out of heaven. Turk's life is far from paradise, and the machinations " +
                "of his psyche can be as perditious as any hellfire. Turk's existence has been marred " +
                "by terrible pain. Traumas fester in his brain like worms, eating him alive from the " +
                "inside out.\n\nIs it possible to return from the brink of annihilation? Can Turk " +
                "banish the darkness inside, or is he doomed to succumb to his misery?");
            SaveHandler.SaveGame();
            ZoneResetter zoneResetter = ServiceLocator.Get<ZoneResetter>(); //TODO: Start in main menu
            yield return zoneResetter.UnloadZone(true); //TODO: Don't unload/generate zone at startup
            */
            ViewManager.Open("Main Menu");
        }


        public static void StartNewGame()
        {
            GameResetter.ResetGame();
            TCDGame game = ServiceLocator.Get<TCDGame>();
            CampaignHandler.StartCampaign(game.defaultCampaignName);
        }

        // TODO: REPLACE THIS WITH ENDING?
        public static void WinGame()
        {

        }

        public static void ExitToDesktop()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            System.Diagnostics.Process.GetCurrentProcess().Kill();
#endif
        }   
    }
}
