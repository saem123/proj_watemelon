using UnityEngine;
using Saem;
namespace Saem
{
    public class StateActive<T> : RxActive
    {
        [SerializeField]
        protected T activeState;

        public virtual void setActiveState(T state)
        {
            activeState = state;
        }

    }
}