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
        protected UnityWebRequest getRequest(string url)
        {
            return UnityWebRequest.Get(server_URL + url);
        }

        protected virtual IObservable<U> get<U>(string url)
        {
            var request = getRequest(url);
            return Observable
                .FromCoroutine<string>(_ => SendRequestCoroutine(_, request))
                .Select(fromString<U>)
                .ObserveOnMainThread();
        }

    }
}