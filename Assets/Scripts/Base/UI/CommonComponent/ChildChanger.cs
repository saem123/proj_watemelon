using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saem;
public class ChildChanger : RxView<int>
{
    GameObject currentChild;

    private void Start()
    {
        int start = 1;
        int count = transform.childCount;
        currentChild = transform.GetChild(0).gameObject;
        for (int index = start; index < count; index++)
        {
            transform.GetChild(index).gameObject.SetActive(false);
        }
        initViewStream();
        initSubscribeStream();
    }
    protected override void publishedValue(int value)
    {
        currentChild.SetActive(false);
        currentChild = transform.GetChild(value).gameObject;
        currentChild.SetActive(true);

    }
}