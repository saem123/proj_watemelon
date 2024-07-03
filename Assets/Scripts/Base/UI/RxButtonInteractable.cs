
using UnityEngine.UI;
namespace Saem
{
    public class RxButtonInteractable : RxUI<Button, bool>
    {

        protected override void publishedValue(bool value)
        {
            applyComponent.interactable = value;
        }
    }

}

