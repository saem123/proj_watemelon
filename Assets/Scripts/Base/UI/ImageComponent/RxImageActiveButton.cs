using UnityEngine;
using UnityEngine.UI;
using Saem;
using UniRx;
using UniRx.Triggers;
namespace Saem
{
    [RequireComponent(typeof(Button))]
    public class RxImageActiveButton : RxButton
    {
        protected Image buttonImage;
        public GameObject overImage;
        public GameObject clickImage;

        protected override void activeSubscribe()
        {
            buttonImage = this.GetComponent<Image>();

            buttonImage
                .OnPointerDownAsObservable()
                .Where(_ => clickImage != null)
                .Subscribe(_ =>
                {
                    if (overImage != null)
                        overImage.SetActive(false);
                    clickImage.SetActive(true);

                })
                .AddTo(gameObject);

            buttonImage
                .OnPointerUpAsObservable()
                .Where(_ => clickImage != null)
                .Subscribe(_ =>
                {
                    if (overImage != null)
                        overImage.SetActive(true);
                    clickImage.SetActive(false);
                })
                .AddTo(gameObject);

        }
    }
}