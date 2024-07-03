using UnityEngine;
using Saem;
namespace Saem
{
    public class StateChangeAlpha<T> : RxComponent<CanvasGroup, T> where T : System.Enum
    {
        public T[] activeStates;

        protected override void publishedValue(T value)
        {
            float alpha = 0;
            for (int index = 0; index < activeStates.Length; index++)
            {
                if (value.Equals(activeStates[index]))
                {
                    alpha = 1;
                    break;
                }

            }
            applyComponent.alpha = alpha;
        }
    }
}