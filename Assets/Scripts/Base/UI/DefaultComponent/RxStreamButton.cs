using UnityEngine.UI;
using UnityEngine;
using System;
using Saem;
using UniRx;
namespace Saem
{
    [RequireComponent(typeof(Button))]
    public class RxStreamButton<T> : RxButton
    {
        public virtual void setClickStream(IObservable<T> stream)
        {

            clickAction = () => { stream.Subscribe().AddTo(gameObject); };

        }

    }
}
