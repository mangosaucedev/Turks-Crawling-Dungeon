using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects;

namespace TCD.UI
{
    public class EmbarkTalentSelection : MonoBehaviour
    {
        [SerializeField] private GameObject nextButton;
        [SerializeField] private TalentView talentView;

        private void OnEnable()
        {
            if (PlayerInfo.currentPlayer)
                Destroy(PlayerInfo.currentPlayer.gameObject);
            PlayerInfo.currentPlayer = ObjectFactory.BuildFromBlueprint("Player", Vector2Int.zero);
            PlayerInfo.currentPlayer.gameObject.SetActive(false);
            PlayerInfo.currentClass = Embark.ChosenClass;
            talentView.BuildTalents();
        }
    }
}
