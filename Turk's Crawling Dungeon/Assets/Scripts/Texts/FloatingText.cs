using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TCD.Texts
{
    public class FloatingText : MonoBehaviour
    {
        private const float SPEED = 1.5f;

        private static List<GameObject> pool = new List<GameObject>();

        public float duration;
        public Text text;

        public static GameObject FromPool()
        {
            if (pool.Count > 0)
            {
                GameObject gameObject = pool[0];
                gameObject.SetActive(true);
                pool.Remove(gameObject);
                return gameObject;
            }
            GameObject prefab = Assets.Get<GameObject>("Floating Text");
            return Instantiate(prefab, ParentManager.Canvas);
        }

        protected virtual void OnEnable()
        {

        }

        protected virtual void FixedUpdate()
        {
            transform.position += Vector3.up * Time.fixedDeltaTime * SPEED;
            if (duration > 0)
                duration -= Time.fixedDeltaTime;
            else
                ReturnToPool();
        }

        private void ReturnToPool()
        {
            if (!pool.Contains(gameObject))
            {
                gameObject.SetActive(false);
                pool.Add(gameObject);
            }
        }
    }
}
