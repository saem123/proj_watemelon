using UnityEngine;
using Saem;
namespace Saem
{
    public class RxAudioVolume : RxUI<AudioSource, float>
    {
        protected override void publishedValue(float value)
        {
            applyComponent.volume = value;
        }
    }

}