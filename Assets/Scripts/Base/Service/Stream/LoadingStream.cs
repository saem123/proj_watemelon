using System.Linq;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Saem;
using System;
namespace Saem
{
    public class LoadingStream : Stream<LoadingStream>
    {
        LoadingService getLoadingService()
        {
            return LoadingService.instance;
        }

        ErrorService getErrorService()
        {
            return ErrorService.instance;
        }
        public IObservable<float> loadingValue()
        {
            return getLoadingService().loadingValue().DoOnError(getErrorService().onError);

        }

        public void loadScene(string sceneName, IObservable<float> prev = null)
        {
            getLoadingService().loadScene(sceneName, prev);
        }
    }
}