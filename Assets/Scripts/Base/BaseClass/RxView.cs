
using UnityEngine;
using Saem;
using UniRx;
using System;

namespace Saem
{
    public class RxView<T> : MonoBehaviour
    {
        protected IObservable<T> viewStream;
        protected virtual void initViewStream() { }
        protected virtual void publishedValue(T value) { }
        IDisposable dispose;
        private void Start()
        {
            initViewStream();
            initSubscribeStream();
        }

        protected void initSubscribeStream()
        {
            if (viewStream != null)
            {
                subscribeStream();
            }
        }

        protected void subscribeStream()
        {
            if (dispose != null)
            {
                dispose.Dispose();
            }
            dispose = viewStream
                .Subscribe(publishedValue)
                .AddTo(gameObject);
        }

        public virtual void setViewStream(IObservable<T> stream)
        {

            viewStream = stream;
            subscribeStream();
        }

    }

}