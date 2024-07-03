using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saem;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;
namespace Saem
{
    public partial class LoadingService : Service<LoadingService>
    {

        string nextScene;

        IObservable<float> prevLoad;

        void setNextScene(string sceneName)
        {
            nextScene = sceneName;

        }

        void setPrev(IObservable<float> prev)
        {
            if (prev == null)
            {
                prevLoad = delayProgress(1.5f);
            }
            else
            {
                prevLoad = delayProgress(1.5f).Concat(prev);
            }
        }

        IObservable<float> delayProgress(float delay)
        {
            return Observable.Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(0.1f))
                                .TakeWhile(_ => _ < delay * 10).Select(_ => (float)_ / (delay * 20.0f));
        }

        IEnumerator loadSceneCoroutine()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(this.nextScene);
            float time = 0f;
            while (!asyncOperation.isDone)
            {
                float fire = 0.5f + asyncOperation.progress / 2.0f;
                yield return fire;
                time += Time.deltaTime;
                if (time > 10.0f) break;
            }
            yield return 1.0f;
        }

    }

}