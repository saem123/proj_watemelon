using UnityEngine;
using Saem;

namespace Saem
{
    public class RxStartGameButton : RxButton
    {
        protected override void initViewStream()
        {
            base.initViewStream();
            setClickAction(() => GameService.instance.StartGame());
        }
    }
} 