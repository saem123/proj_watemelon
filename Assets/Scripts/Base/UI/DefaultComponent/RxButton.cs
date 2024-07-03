using UnityEngine.UI;
using UnityEngine;
using UniRx;
using System;
namespace Saem
{
    [RequireComponent(typeof(Button))]
    public class RxButton : RxUI<Button, Unit>
    {
        protected Action clickAction;
        protected virtual void onClick() { if (clickAction != null) clickAction(); }
        protected virtual void activeSubscribe() { }

        protected override void initViewStream()
        {
            viewStream = applyComponent.OnClickAsObservable();
            alphaSkipClick();
            activeSubscribe();

        }

        protected override void publishedValue(Unit value)
        {
            onClick();
        }

        void alphaSkipClick()
        {

            Image image = applyComponent.GetComponent<Image>();
            if (image != null && image.sprite != null && image.sprite.texture.isReadable)
                image.alphaHitTestMinimumThreshold = 0.1f;
        }

        public void setClickAction(Action click)
        {
            clickAction = click;
        }



    }
}

