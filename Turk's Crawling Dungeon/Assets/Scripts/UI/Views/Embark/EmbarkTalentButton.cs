using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TCD.Objects.Parts.Talents;

namespace TCD.UI
{
    public class EmbarkTalentButton : MonoBehaviour, ITalentButton
    {
        [SerializeField] private Image icon;
        [SerializeField] private Text label;

        private Talent talent;

        private void Start()
        {
            UpdateLabel();
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
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            int level = Embark.GetChosenTalentLevel(talent.GetType().Name);
            label.text = $"{level} / {talent.MaxLevel}";
        }

        public void OnPointerEnter(PointerEventData eventData)
        {

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
