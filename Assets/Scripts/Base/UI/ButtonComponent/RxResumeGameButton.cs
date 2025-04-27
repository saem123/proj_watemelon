using UnityEngine;
using Saem;

namespace Saem
{
    public class RxResumeGameButton : RxButton
    {
        protected override void initViewStream()
        {
            base.initViewStream();
            setClickAction(() => GameService.instance.ResumeGame());
        }
    }
} 