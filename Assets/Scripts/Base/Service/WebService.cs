using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
namespace Saem
{

    public partial class WebService<T> : Service<T> where T : class, new()
    {
        bool isDebug = false;

#if UNITY_EDITOR
        protected string server_URL = "http://localhost";
#else
        protected string server_URL = "";
#endif
    }
}