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
            EventManager.Listen<EmbarkTalentPointModifiedEvent>(this, OnEmbarkTalentPointModified);
            RebuildPlayer();
            talentView.BuildTalents();
        }

        private void OnDisable()
        {
            EventManager.StopListening<EmbarkTalentPointModifiedEvent>(this);
        }

        private void OnEmbarkTalentPointModified(EmbarkTalentPointModifiedEvent e)
        {
            talentView.UpdateTalentPointCount();
            if (PlayerInfo.talentPoints == 0)
                nextButton.SetActive(true);
            else if (!nextButton.activeInHierarchy)
                nextButton.SetActive(false);
        }

        private void RebuildPlayer()
        {
            if (PlayerInfo.currentPlayer)
                Destroy(PlayerInfo.currentPlayer.gameObject);
            PlayerInfo.currentPlayer = ObjectFactory.BuildFromBlueprint("Player", Vector2Int.zero);
            PlayerInfo.currentPlayer.gameObject.SetActive(false);
            PlayerInfo.currentClass = Embark.ChosenClass;
        }
    }
}
