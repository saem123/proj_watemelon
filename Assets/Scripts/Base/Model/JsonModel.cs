using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Saem;
using UniRx;
namespace Saem
{
    [Serializable]
    public class JsonClass<T>
    {
        public T data;
    }

}