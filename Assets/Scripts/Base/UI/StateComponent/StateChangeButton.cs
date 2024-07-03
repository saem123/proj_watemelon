using UnityEngine;
using Saem;
namespace Saem
{
    public class StateChangeButton<T> : RxButton where T : System.Enum
    {
        [SerializeField]
        protected T changeState;
        protected virtual void setState(T state) { }

        protected override void onClick()
        {
            setState(changeState);
        }
    }
}