
using UnityEngine;
using Saem;
namespace Saem
{
    public class RxComponent<U, T> : RxView<T> where U : Component
    {
        protected U applyComponent;

        void Awake()
        {
            applyComponent = GetComponent<U>();
        }
    }

}