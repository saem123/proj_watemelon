using UnityEngine;
using Saem;

namespace Saem
{
    public class RxGameProgressSlider : RxSliderValue
    {
        protected override void initViewStream()
        {
            viewStream = GameService.instance.OnGameProgressChanged;
        }
    }
} 