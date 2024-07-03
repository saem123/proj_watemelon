using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Saem;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;
namespace Saem
{

    public class ErrorService : Service<ErrorService>
    {
        ReactiveProperty<string> errorMsg = new ReactiveProperty<string>();

        ReactiveProperty<bool> errorActive = new ReactiveProperty<bool>();

        Action action = null;
        public void onError(Exception exception)
        {
            action = null;
            errorMsg.Value = getErrorMessage(exception);
        }

        public void onError(string exception)
        {
            action = null;
            errorMsg.Value = exception;
        }
        public void onError(string exception, Action act)
        {
            action = null;
            action = act;
            errorMsg.Value = exception;
        }

        public IObservable<string> getErrorMessageStream()
        {
            return errorMsg.Where(_ => _ != null).Do(_ => errorActive.Value = true);
        }

        public IObservable<bool> getErrorPopupActiveStream()
        {
            return errorActive;
        }

        public void offErrorPopup()
        {
            errorActive.Value = false;
            errorMsg.Value = null;
            if (action != null)
            {
                action.Invoke();
            }
        }

        string getErrorMessage(Exception exception)
        {
            string message = "";
            if (exception.GetType().Equals(
                typeof(System.Exception)
            ))
            {
                message = exception.Message;
            }
            else
            {
                message = exception.ToString();
            }
            return exceptionMessage2Local(message);
        }

        string exceptionMessage2Local(string message)
        {
            if (message.IndexOf("Protocol") != -1)
            {
                return "인증되지 않은 접근입니다.";
            }

            if (message.IndexOf("Connection") != -1)
            {
                return "연결이 불안정합니다.";
            }
            return message;
        }
    }
}