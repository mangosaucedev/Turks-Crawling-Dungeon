using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCD.Indicators
{
    public class IndicatorSprite : MonoBehaviour
    {
        private static List<GameObject> pool = new List<GameObject>();

        public Sprite sprite;

        [SerializeField] private SpriteRenderer spriteRenderer;

        private SpriteRenderer SpriteRenderer
        {
            get
            {
                if (!spriteRenderer)
                    spriteRenderer = GetComponent<SpriteRenderer>();
                return spriteRenderer;
            }
        }

        public static IndicatorSprite Draw(Vector2Int position, Sprite sprite)
        {
            GameObject gameObject = FromPool();
            IndicatorSprite indicatorSprite = gameObject.GetComponent<IndicatorSprite>();
            indicatorSprite.transform.position = (Vector2) position * Cell.SIZE + (new Vector2(Cell.SIZE, Cell.SIZE) / 2);
            indicatorSprite.SetSprite(sprite);
            return indicatorSprite;
        }

        private static GameObject FromPool()
        {
            if (pool.Count > 0)
            {
                GameObject gameObject = pool[0];
                if (gameObject)
                {
                    gameObject.SetActive(true);
                    pool.Remove(gameObject);
                    return gameObject;
                }
            }
            GameObject prefab = Assets.Get<GameObject>("Indicator Sprite");
            return Instantiate(prefab, ParentManager.Temp);
        }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
            if (!pool.Contains(gameObject))
                pool.Add(gameObject);
        }

        public void SetSprite(Sprite sprite)
        {
            SpriteRenderer.sprite = sprite;
        }
    }
}
