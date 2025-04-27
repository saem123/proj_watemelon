using UnityEngine;
using Saem;

namespace Saem
{
    public class RxPauseGameButton : RxButton
    {
        protected override void initViewStream()
        {
            base.initViewStream();
            setClickAction(() => GameService.instance.PauseGame());
        }
    }
} 