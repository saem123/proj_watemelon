using UnityEngine;
using Saem;
using UnityEngine.UI;
using UniRx;
namespace Saem
{
    public class SpriteScroller : MonoBehaviour
    {
        public float scrollSpeedX = 10;
        public float scrollSpeedY = 10;

        Image image;
        private Vector2 savedOffset;
        protected float direction = 1;

        void Start()
        {
            image = GetComponent<Image>();
            directionChangeStream();
        }

        protected virtual void directionChangeStream()
        {

        }

        void Update()
        {
            float x = Mathf.Repeat(Time.time * scrollSpeedX * direction, 1);
            float y = Mathf.Repeat(Time.time * scrollSpeedY * direction, 1);
            Vector2 offset = Vector2.right * x + Vector2.up * y;
            image.material.SetTextureOffset("_MainTex", offset);
        }
    }
}
