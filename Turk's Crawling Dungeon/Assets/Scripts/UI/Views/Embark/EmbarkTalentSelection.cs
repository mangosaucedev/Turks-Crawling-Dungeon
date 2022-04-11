using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects.Parts.Talents;

namespace TCD.UI
{
    public class EmbarkTalentSelection : MonoBehaviour
    {
        [SerializeField] private GameObject nextButton;
        [SerializeField] private TalentView talentView;

        private void Awake()
        {
            talentView.style = TalentDescriptionStyle.EmbarkView;
        }

        private void OnEnable()
        {
            EventManager.Listen<EmbarkTalentPointModifiedEvent>(this, OnEmbarkTalentPointModified);
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
    }
}
