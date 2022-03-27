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

        public void OnPointerEnter(PointerEventData eventData)
        {

        }

        public void OnPointerClick(PointerEventData eventData)
        {

        }

        public void OnSubmit(BaseEventData eventData)
        {

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
