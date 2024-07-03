using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
namespace Saem
{

    public partial class WebService<T> : Service<T> where T : class, new()
    {
        protected UnityWebRequest postRequest(string url, WWWForm postData)
        {
            return UnityWebRequest.Post(server_URL + url, postData);
        }


        protected UnityWebRequest postRequest<ResponseType, RequestType>(string url, RequestType postData, ResponseType defaultResponse = null) where ResponseType : class, new()
        {
            RequestModel<ResponseType, RequestType> requestModel = makeRequestModel(postData, new ResponseType());
            string jsonString = JsonUtility.ToJson(requestModel);
            UnityWebRequest req = UnityWebRequest.Post(server_URL + url, jsonString);

            byte[] jsonToSend = new UTF8Encoding().GetBytes(jsonString);
            req.uploadHandler = new UploadHandlerRaw(jsonToSend);
            req.SetRequestHeader("Content-Type", "application/json");
            req.SetRequestHeader("ModelType", typeof(RequestType).ToString());
            req.SetRequestHeader("authorization", CommonUtility.getString(BaseDataKey.AUTH_TOKEN, ""));
            return req;

        }

        protected virtual IObservable<ResponseType> post<ResponseType>(string url, ResponseType postData) where ResponseType : class, new()
        {

            return post(url, postData, postData);
        }

        protected virtual IObservable<ResponseType> post<ResponseType, RequestType>(string url, RequestType postData, ResponseType defaultResponse = null) where ResponseType : class, new()
        {

            var request = postRequest<ResponseType, RequestType>(url, postData, defaultResponse);
            return Observable
                .FromCoroutine<string>(_ => SendRequestCoroutine(_, request))
                .Select(fromString<ResponseModel<ResponseType>>)
                .Where(checkResponse)
                .Select(_ => _.response)
                .ObserveOnMainThread();
        }


    }
}