using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saem;
public class ChildStateChanger<T> : RxView<T> where T : System.Enum
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
    protected virtual int convert(T value)
    {
        int result = CommonUtility.getEnumValue(value);

        return result;
    }
    protected override void publishedValue(T value)
    {
        currentChild.SetActive(false);
        currentChild = transform.GetChild(convert(value)).gameObject;
        currentChild.SetActive(true);

    }
}