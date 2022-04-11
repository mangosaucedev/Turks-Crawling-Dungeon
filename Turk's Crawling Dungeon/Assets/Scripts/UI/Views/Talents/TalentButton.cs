using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TCD.Objects.Parts.Talents;
using TCD.UI.Tooltips;

namespace TCD.UI
{
    public class TalentButton : MonoBehaviour, ITalentButton
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image icon;
        [SerializeField] private Text label;

        private TalentView view;
        private Talent talent;

        public TalentView View
        {
            get => view;
            set => view = value;
        }

        public Talent Talent => talent;

        public int TalentLevel => talent.level;

        private void Start()
        {
            UpdateButton();
        }

        private void OnEnable()
        {
            EventManager.Listen<TalentPointModifiedEvent>(this, OnTalentPointModified);
        }

        private void OnDisable()
        {
            EventManager.StopListening<TalentPointModifiedEvent>(this);
        }

        private void OnTalentPointModified(TalentPointModifiedEvent e)
        {
            UpdateButton();
        }

        private void UpdateButton()
        {
            label.text = $"{TalentLevel} / {talent.MaxLevel}";

            if (!talent.MeetsRequirements(TalentLevel))
                DeactivateButton();
            else
                ActivateButton();
        }

        private void DeactivateButton()
        {
            while (TalentLevel > 0)
                TalentUtility.RemoveTalentPoint(talent);
            canvasGroup.interactable = false;
            canvasGroup.alpha = 0.5f;
        }

        private void ActivateButton()
        {
            canvasGroup.interactable = true;
            canvasGroup.alpha = 1f;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            View.UpdateDescription(Talent, TalentLevel);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                OnSubmit(eventData);
            if (eventData.button == PointerEventData.InputButton.Right)
                RemoveTalentPoint();
        }

        public void OnSubmit(BaseEventData eventData)
        {
            if (TalentLevel < talent.MaxLevel)
                TalentUtility.AddTalentPoint(talent);
        }

        private void RemoveTalentPoint()
        {
            if (TalentLevel > 0)
                TalentUtility.RemoveTalentPoint(talent);
        }

        public void BuildTalent(Talent talent)
        {
            this.talent = talent;
            UpdateTalent();
        }

        private void UpdateTalent()
        {
            icon.sprite = talent.Icon;
        }
    }
}
