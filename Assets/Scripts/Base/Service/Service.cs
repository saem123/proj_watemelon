using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saem;
namespace Saem
{
    public class Service<T> where T : class, new()
    {
        private static T _instance = null;

        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
                return _instance;

            }
        }

        protected static void destroyInstance()
        {
            _instance = null;
        }
    }
}