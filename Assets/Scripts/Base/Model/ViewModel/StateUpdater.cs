using System;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Saem;
namespace Saem
{
    public class StateUpdater<T> : StateViewModel<T> where T : Enum
    {
        Dictionary<T, Func<T, bool>> stateConditionDictionary = new Dictionary<T, Func<T, bool>>();

        public void setStateCondition(T state, Func<T, bool> condition)
        {
            if (stateConditionDictionary.ContainsKey(state))
            {
                stateConditionDictionary[state] = condition;
            }
            else
            {
                stateConditionDictionary.Add(state, condition);
            }
        }

        public void removeState(T state)
        {
            stateConditionDictionary.Remove(state);
        }

        public void setStateUpdater(MonoBehaviour updater)
        {
            updater.UpdateAsObservable().Subscribe(stateUpdate).AddTo(updater.gameObject);
        }

        protected void stateUpdate(Unit unit)
        {
            foreach (var item in stateConditionDictionary)
            {
                if (item.Value(getState()))
                {
                    changeState(item.Key);
                }
            }
        }

    }
}