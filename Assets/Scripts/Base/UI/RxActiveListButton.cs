using UnityEngine;
using UnityEngine.UI;
using Saem;
using UniRx;
using UniRx.Triggers;
using System;
using System.Collections.Generic;
namespace Saem
{
    public class RxActiveListButton : RxButton
    {
        [SerializeField]
        public List<GameObject> pressedActiveObjectList = new List<GameObject>();
        [SerializeField]
        public List<GameObject> enteredActiveObjectList = new List<GameObject>();

        protected Button observableTarget;

        protected override void activeSubscribe()
        {
            if (observableTarget == null)
            {
                observableTarget = GetComponent<Button>();
                observableTarget.transition = Selectable.Transition.None;
            }


            if (observableTarget == null) return;


            observableTarget.OnPointerDownAsObservable()
                .Subscribe(_ =>
                {
                    pressed();
                })
                .AddTo(gameObject);

            observableTarget.OnPointerEnterAsObservable()
            .Subscribe(_ =>
                {
                    entered();
                })
                .AddTo(gameObject);
            observableTarget.OnPointerUpAsObservable()
                .Subscribe(_ =>
                {
                    released();
                })
                .AddTo(gameObject);

            observableTarget.OnPointerExitAsObservable()
                .Subscribe(_ =>
                {
                    exited();
                })
                .AddTo(gameObject);

            exited();
        }

        public virtual void pressed()
        {
            pressedActiveObjectList.ForEach(_ => _.SetActive(true));
        }

        public virtual void entered()
        {
            enteredActiveObjectList.ForEach(_ => _.SetActive(true));
        }
        public virtual void released()
        {
            pressedActiveObjectList.ForEach(_ => _.SetActive(false));
        }

        public virtual void exited()
        {
            pressedActiveObjectList.ForEach(_ => _.SetActive(false));
            enteredActiveObjectList.ForEach(_ => _.SetActive(false));
        }

        public void addPressedObject(GameObject activeObject)
        {
            activeObject.SetActive(false);
            pressedActiveObjectList.Add(activeObject);
        }

        public void removePressedObject(GameObject activeObject)
        {
            pressedActiveObjectList.Remove(activeObject);
        }

        public void addDefaultObject(GameObject activeObject)
        {
            activeObject.SetActive(true);
            //enteredActiveObjectList.Add(activeObject);
        }

        public void removeDefaultObject(GameObject activeObject)
        {
            enteredActiveObjectList.Remove(activeObject);
        }



    }
}