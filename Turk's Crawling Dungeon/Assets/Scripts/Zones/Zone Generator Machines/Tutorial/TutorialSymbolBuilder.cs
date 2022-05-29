using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCD.Objects;
using TCD.Zones.Templates;

namespace TCD.Zones
{
    public class TutorialSymbolBuilder : TutorialTemplateMachine
    {
        public override IEnumerator Generate()
        {
            PlacePlayer();
            PlaceTutorialNpc();
            PlaceFirstDoor();
            PlaceLockedDoor();
            PlaceCrowbarSpawner();
            yield return null;
        }

        private void PlacePlayer()
        {
            var symbol = GetZoneTemplate().GetSymbols('P')[0];
            var position = symbol.position;
            if (!PlayerInfo.currentPlayer)
            {
                PlayerUtility.BuildPlayer("Player", position);
                EventManager.Send(new PlayerCreatedEvent());
            }
            else
                PlayerInfo.currentPlayer.cell.SetPosition(position);
        }

        private void PlaceTutorialNpc()
        {
            var symbol = GetZoneTemplate().GetSymbols('L')[0];
            var position = symbol.position;
            ObjectFactory.BuildFromBlueprint("TutorialNpc", position);
        }

        private void PlaceFirstDoor()
        {
            var symbol = GetZoneTemplate().GetSymbols('!')[0];
            var position = symbol.position;
            ObjectFactory.BuildFromBlueprint("TutorialDoor0", position);
        }

        private void PlaceLockedDoor()
        {
            var symbol = GetZoneTemplate().GetSymbols('@')[0];
            var position = symbol.position;
            ObjectFactory.BuildFromBlueprint("TutorialDoor1", position);
        }

        private void PlaceCrowbarSpawner()
        {
            var symbol = GetZoneTemplate().GetSymbols('i')[0];
            var position = symbol.position;
            ObjectFactory.BuildFromBlueprint("TutorialCrowbarSpawner", position);
        }
    }
}
