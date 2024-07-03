using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasAutoMainCam : MonoBehaviour
{
    private void Awake()
    {
        Canvas canvas = GetComponent<Canvas>();
        if (
            canvas.worldCamera == null
        )
        {
            canvas.worldCamera = Camera.main;
        }



    }
}
