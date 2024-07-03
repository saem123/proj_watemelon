using UnityEngine;
using Saem;
namespace Saem
{
    public class StateChangeActives<T> : RxView<T> where T : System.Enum
    {
        public T[] activeStates;

        protected override void publishedValue(T value)
        {
            bool active = false;
            for (int index = 0; index < activeStates.Length; index++)
            {
                if (value.Equals(activeStates[index]))
                {
                    active = true;
                    break;
                }

            }
            gameObject.SetActive(active);
        }
    }
}