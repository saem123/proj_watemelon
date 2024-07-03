
using UnityEngine.UI;
namespace Saem
{
    public class RxSlider : RxUI<Slider, float>
    {
        protected override void publishedValue(float value)
        {
            applyComponent.value = value;
        }
    }
}
