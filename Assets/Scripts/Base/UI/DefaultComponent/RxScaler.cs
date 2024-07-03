using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Saem
{
    public class RxScaler : RxUI<RectTransform, bool>
    {
        protected override void publishedValue(bool value)
        {
            if (value == true)
                applyComponent.localScale = trueScale();
            else
                applyComponent.localScale = falseScale();
        }
        protected virtual Vector3 trueScale()
        {
            return Vector3.one;
        }
        protected virtual Vector3 falseScale()
        {
            return new Vector3(0.5f, 0.5f, 0.5f);
        }

    }
}
