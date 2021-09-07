using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Objects.Parts
{
    [Serializable]
    public abstract class Graphic : Part
    {
        protected GameObject graphicObject;
        protected SpriteRenderer graphicRenderer;

        [SerializeField] private int renderOrder;

        public int RenderOrder
        {
            get => renderOrder;
            set => renderOrder = value;
        }

        protected abstract Sprite Sprite { get; }
        
        protected override void Start()
        {
            base.Start();
            SetupGraphic();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (graphicObject && !graphicObject.activeInHierarchy)
                graphicObject.SetActive(true);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (graphicObject && graphicObject.activeInHierarchy)
                graphicObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if (graphicObject)
                Destroy(graphicObject);
        }

        protected virtual void SetupGraphic()
        {
            GameObject prefab = Assets.Get<GameObject>("Graphic");
            graphicObject = Instantiate(prefab, parent.transform);
            graphicRenderer = graphicObject.GetComponent<SpriteRenderer>();
            graphicRenderer.sprite = Sprite;
        }
    }
}
