using System;

using System.Collections.Generic;
using UnityEngine;
using Saem;
using UniRx;
namespace Saem
{
    public class FakeWebService<U> : WebService<U> where U : class, new()
    {
        
        protected IEnumerator<AsyncOperation> loadRequestCoroutine(IObserver<string> webData, string url)
        {
            ResourceRequest req = Resources.LoadAsync<TextAsset>(url);
            yield return req;
            TextAsset textAsset = (TextAsset)req.asset;
            string dataString = textAsset.text;

            webData.OnNext(dataString);
            webData.OnCompleted();
        }

        protected override IObservable<T> get<T>(string url)
        {

            return Observable
                .FromCoroutine<string>(_ => loadRequestCoroutine(_, "server" + url))
                .Select(fromString<T>)
                .ObserveOnMainThread();
        }

        public IObservable<T> getJSon<T>(string url)
        {
            return get<T>(url);
        }

    }
}