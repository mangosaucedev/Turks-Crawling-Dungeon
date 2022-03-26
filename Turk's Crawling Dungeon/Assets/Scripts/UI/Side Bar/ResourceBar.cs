using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCD.Objects;
using TCD.Objects.Parts;
using Resources = TCD.Objects.Parts.Resources;

namespace TCD.UI
{
    public class ResourceBar : MonoBehaviour
    {
        private static Dictionary<Resource, Color> colors;

        public Resource resource;
        public Text text;

        [SerializeField] private Image bar;
        
        private float value;
        private float max;
        private float percent;
        private float regen;
        private bool isRegenerating;

        private BaseObject Player => PlayerInfo.currentPlayer;

        private Resources Resources => Player.Parts.Get<Resources>();

        static ResourceBar()
        {
            colors = new Dictionary<Resource, Color>() 
            {
                {Resource.Hitpoints, Color.red},
                {Resource.Stamina, new Color(1f, 1f, 0.75f)},
                {Resource.Psi, Color.blue}, 
                {Resource.Oxygen, Color.cyan},
                {Resource.Hunger, new Color(0.65f, 0.15f, 0.15f)},
                {Resource.Thirst, new Color(0.25f, 0.6f, 0.7f)},
                {Resource.Morale, Color.Lerp(Color.red, Color.blue, 0.5f)},
                {Resource.Stimulation, Color.Lerp(Color.red, Color.yellow, 0.5f)},
            };
        }

        public void Initialize()
        {
            value = Resources.GetResource(resource);
            max = Resources.GetMaxResource(resource);
            regen = Resources.GetResourceRegen(resource);
            float regenPoint = Resources.GetResourceRegenPoint(resource);
            percent = Mathf.Clamp01(value / max);
            isRegenerating = percent < regenPoint;
            bar.color = colors[resource];
            bar.fillAmount = percent;
        }

        public void ShowText(bool showAltText)
        {
            if (!showAltText)
                text.text = resource.ToString();
            else
                text.text = $"{value.RoundToDecimal(1)} / {max.RoundToDecimal(1)}" + (isRegenerating ? $" (+{regen.RoundToDecimal(1)})" : "");
        }
    }
}
