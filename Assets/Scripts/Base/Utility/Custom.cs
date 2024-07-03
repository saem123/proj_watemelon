using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Saem
{
    public class Custom
    {

        public static void Log(object message)
        {
#if UNITY_EDITOR
            Debug.Log(message);
#endif
        }


    }
}