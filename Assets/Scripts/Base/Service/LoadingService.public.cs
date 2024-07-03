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

        public void loadScene(string sceneName, IObservable<float> prev = null)
        {
            setNextScene(sceneName);
            setPrev(prev);
            SceneManager.LoadScene(BaseConstants.SCENE_LOADING);
        }

        public IObservable<float> loadingValue()
        {

            IObservable<float> sceneProgress = Observable.FromCoroutineValue<float>(loadSceneCoroutine);
            return prevLoad.Concat(sceneProgress);

        }


    }

}