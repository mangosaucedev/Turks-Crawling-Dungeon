using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Texts
{
    public class FloatingTextFlying : FloatingText
    {
        private const float SPEED = 1.5f;

        private static List<GameObject> pool = new List<GameObject>();

        private float xSpeed;

        protected override void OnEnable()
        {
            base.OnEnable();
            xSpeed = Random.Range(-SPEED, SPEED);
        }

        protected override void FixedUpdate()
        {
            transform.position += Vector3.right * Time.fixedDeltaTime * xSpeed;
            base.FixedUpdate();
        }

        public new static GameObject FromPool()
        {
            if (pool.Count > 0)
            {
                GameObject gameObject = pool[0];
                gameObject.SetActive(true);
                pool.Remove(gameObject);
                return gameObject;
            }
            GameObject prefab = Assets.Get<GameObject>("Floating Text Flying");
            return Instantiate(prefab, ParentManager.Canvas);
        }
    }
}
