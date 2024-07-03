using System;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Saem;
namespace Saem
{
    public class StateViewModel<T> : ViewModel where T : Enum
    {
        ReactiveProperty<T> modelState = new ReactiveProperty<T>();
        public IObservable<T> stateStream()
        {
            return modelState;
        }

        public IObservable<T> stateStream(T value)
        {
            return modelState.Where(state => state.Equals(value));
        }

        protected T changeState(T state)
        {
            return modelState.Value = state;
        }

        public T getState()
        {
            return modelState.Value;
        }

        public bool isState(T value)
        {
            return getState().Equals(value);
        }
    }
}