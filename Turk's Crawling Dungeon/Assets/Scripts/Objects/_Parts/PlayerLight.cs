using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


namespace TCD.Objects.Parts
{
    public class PlayerLight : Part
    {
        [SerializeField] private string lightName;
        [SerializeField] private float intensity;

        public override string Name => "Sprite Light";

        public string LightName
        {
            get => lightName;
            set => lightName = value;
        }

        public float Intensity
        {
            get => intensity;
            set => intensity = value;
        }

        protected override void Start()
        {
            base.Start();
            Transform lightParent = parent.SpriteRenderer.transform;
            GameObject gameObject = Instantiate(Assets.Get<GameObject>(LightName), lightParent);
            Light2D light = gameObject.GetComponentInChildren<Light2D>();
            light.intensity = intensity;
        }
    }
}
