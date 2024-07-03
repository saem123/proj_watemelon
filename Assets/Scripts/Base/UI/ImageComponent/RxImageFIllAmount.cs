namespace Saem
{
    public class RxImageFillAmount : RxImage<float>
    {
        protected override void publishedValue(float value)
        {
            applyComponent.fillAmount = value;
        }
    }
}