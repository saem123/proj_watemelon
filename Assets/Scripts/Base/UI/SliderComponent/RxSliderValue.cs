namespace Saem
{
    public class RxSliderValue : RxSlider<float>
    {
        protected override void publishedValue(float value)
        {
            applyComponent.value = value;
        }
    }
} 