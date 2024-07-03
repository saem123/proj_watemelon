
using UnityEngine;
using Saem;
namespace Saem
{
    public class RxImageSprite : RxImage<Sprite>
    {

        protected override void publishedValue(Sprite value)
        {
            applyComponent.enabled = value != null;
            applyComponent.sprite = value;

        }

        public void setSprite(Sprite value)
        {
            publishedValue(value);
        }
    }
}