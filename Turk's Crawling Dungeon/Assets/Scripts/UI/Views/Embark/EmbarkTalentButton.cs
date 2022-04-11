using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TCD.Objects.Parts.Talents;
using TCD.UI.Tooltips;

namespace TCD.UI
{
    public class EmbarkTalentButton : MonoBehaviour, ITalentButton
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

        public int TalentLevel => Embark.GetChosenTalentLevel(Talent.Name);

        private void Start()
        {
            UpdateButton();
        }

        private void OnEnable()
        {
            EventManager.Listen<EmbarkTalentPointModifiedEvent>(this, OnEmbarkTalentPointModified);
        }

        private void OnDisable()
        {
            EventManager.StopListening<EmbarkTalentPointModifiedEvent>(this);
        }

        private void OnEmbarkTalentPointModified(EmbarkTalentPointModifiedEvent e)
        {
            UpdateButton();
        }

        private void UpdateButton()
        {
            int level = Embark.GetChosenTalentLevel(talent.GetType().Name);
            label.text = $"{level} / {talent.MaxLevel}";
            
            if (!talent.MeetsEmbarkRequirements(level))
                DeactivateButton();
            else
                ActivateButton();
        }

        private void DeactivateButton()
        {
            while (Embark.GetChosenTalentLevel(talent.GetType().Name) > 0)
                Embark.RemoveChosenTalentLevel(talent.GetType().Name);
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
            string name = talent.GetType().Name;
            int level = Embark.GetChosenTalentLevel(name);
            if (level < talent.MaxLevel)
                Embark.AddChosenTalentLevel(name);
        }

        private void RemoveTalentPoint()
        {
            string name = talent.GetType().Name;
            int level = Embark.GetChosenTalentLevel(name);
            if (level > 0)
                Embark.RemoveChosenTalentLevel(name);
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
