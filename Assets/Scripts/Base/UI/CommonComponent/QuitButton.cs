using UnityEngine;
using Saem;
namespace Saem
{
    public class QuitButton : RxButton
    {
        protected override void onClick()
        {
            Application.Quit();
        }
    }
}
