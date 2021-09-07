using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    public class Render : Part
    {
        [SerializeField] private string sprite;
        [SerializeField] private int renderOrder;
        [SerializeField] private string displayName;
        [SerializeField] private string displayNamePlural;
        [SerializeField] private string description;
        private Dictionary<string, Color> colorLayersByName = new Dictionary<string, Color>();
        private List<Color> colorLayers = new List<Color>();
        private string baseColor;

        public string Sprite
        {
            get => sprite;
            set => sprite = value;
        }

        public string BaseColor
        {
            get => baseColor;
            set => baseColor = value;
        }

        public int RenderOrder
        {
            get => renderOrder;
            set => renderOrder = value;
        }

        public string DisplayName
        {
            get => displayName;
            set => displayName = value;
        }

        public string DisplayNamePlural
        {
            get => displayNamePlural;
            set => displayNamePlural = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }

        public override string Name => "Render";

        protected override void Start()
        {
            base.Start();

            SetSprite(Sprite);
            if (GetBaseColor() != Color.white)
                UpdateColor();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            parent.cell.SetPositionEvent += cell => UpdateRenderOrder(cell);
            UpdateRenderOrder(parent.cell.CurrentCell);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            parent.cell.SetPositionEvent -= cell => UpdateRenderOrder(cell);
        }

        public void SetSprite(string spriteName)
        {
            Sprite sprite = Assets.Get<Sprite>(this.sprite);
            SetSprite(sprite);
        }

        public void SetSprite(Sprite sprite)
        {
            parent.SpriteRenderer.sprite = sprite;
        }

        public Color GetBaseColor()
        {
            if (!ColorUtility.TryParseHtmlString(BaseColor, out Color color))
                color = Color.white;
            return color;
        }

        private void UpdateColor()
        {
            Color color = GetBaseColor();
            foreach (Color colorLayer in colorLayers)
                color = Color.Lerp(color, colorLayer, 0.5f);
            parent.SpriteRenderer.color = color;
        }

        public void AddColorLayer(string layerName, Color color)
        {
            if (!colorLayersByName.ContainsKey(layerName))
            {
                colorLayers.Add(color);
                colorLayersByName.Add(layerName, color);
                UpdateColor();
            }
        }

        public void RemoveColorLayer(string layerName)
        {
            if (colorLayersByName.TryGetValue(layerName, out Color color))
            {
                colorLayers.Remove(color);
                colorLayersByName.Remove(layerName);
                UpdateColor();
            }
        }

        private void UpdateRenderOrder(Cell cell)
        {
            if (parent)
                parent.SpriteRenderer.sortingOrder = (-cell?.Y ?? 0) + RenderOrder;
        } 

        public void RenderEffect(string effectName, string spriteName, float duration)
        {
            StartCoroutine(RenderEffectRoutine(effectName, spriteName, duration));
        }

        private IEnumerator RenderEffectRoutine(string effectName, string spriteName, float duration)
        {
            GameObject prefab = Assets.Get<GameObject>(effectName);
            Sprite sprite = Assets.Get<Sprite>(spriteName);
            GameObject obj = Instantiate(prefab, transform);
            RenderEffect renderEffect = obj.GetComponent<RenderEffect>();
            renderEffect.SetSprite(sprite);
            yield return new WaitForSeconds(duration);
            Destroy(obj);
        }
    }
}
