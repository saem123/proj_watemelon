using UnityEngine;
using Saem;
using System.Collections;
using UniRx;
using UniRx.Triggers;

namespace Saem
{
    public class CamRatio : MonoBehaviour
    {
        public float defaultWidth = 720;
        public float defaultHeight = 1280;
        void Start()
        {

            cameraRatio();

#if UNITY_WEBGL

        this.UpdateAsObservable()
            .Select(_ => Screen.fullScreen)
            .Pairwise()
            .Where(_ => _.Current != _.Previous)
            .Subscribe(_ => cameraRatio())
            .AddTo(this);
        
#endif
        }

        public void cameraRatio()
        {
            Camera cam = GetComponent<Camera>();
            float ratio = defaultWidth / defaultHeight;
            float screenRatio = (float)Screen.width / (float)Screen.height;
            float width = screenRatio / ratio > 1 ? ratio / screenRatio : 1;
            float height = screenRatio / ratio > 1 ? 1 : screenRatio / ratio;
            Rect camRect = cam.rect;
            camRect.width = width;
            camRect.height = height;
            camRect.x = (1 - width) / 2.0f;
            camRect.y = (1 - height) / 2.0f;
            cam.rect = camRect;
        }

        void OnPreCull() => GL.Clear(true, true, Color.black);


    }
}