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
        protected virtual IEnumerator<UnityWebRequestAsyncOperation> SendRequestCoroutine(IObserver<string> webData, UnityWebRequest request)
        {

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string dataString = Encoding.Default.GetString(request.downloadHandler.data);
                webData.OnNext(dataString);
                webData.OnCompleted();
            }
            else
            {
                webData.OnError(new Exception(request.result.ToString()));
            }


        }

        protected virtual U fromString<U>(string dataString)
        {
            string fileName = typeof(U).ToString();
            if (isDebug)
            {
                CommonUtility.jsonFileMaker("/" + SceneManager.GetActiveScene().name + "/" + fileName, dataString);
            }

            string jsonString = "{ \"data\":" + dataString + "}";
            return JsonUtility.FromJson<JsonClass<U>>(jsonString).data;
        }

        public RequestModel<ResponseType, RequestType> makeRequestModel<ResponseType, RequestType>(RequestType request, ResponseType defaultResponse)
        {
            RequestModel<ResponseType, RequestType> requestModel = new RequestModel<ResponseType, RequestType>();
            ResponseModel<ResponseType> responseModel = new ResponseModel<ResponseType>();
            responseModel.response = defaultResponse;
            requestModel.request = request;
            requestModel.response = responseModel;
            return requestModel;
        }

        public bool checkResponse<ResponseType>(ResponseModel<ResponseType> responseModel)
        {
            if (responseModel.responseCode.Equals("fail"))
            {
                ErrorService.instance.onError(responseModel.responseMessage);
                return false;
            }

            return true;
        }
    }
}