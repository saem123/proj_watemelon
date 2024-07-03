using System;
namespace Saem
{
    public class SceneLoadButton : RxButton
    {
        public string sceneName = "LobbyScene";

        protected IObservable<float> prevLoad;
        protected override void onClick()
        {
            LoadingStream.instance.loadScene(sceneName, prevLoad);
        }

    }
}
