using System;
using System.Collections.Generic;


namespace Saem
{
    [Serializable]
    public class RequestModel<ResponseType, RequestType>
    {
        public RequestType request;
        public ResponseModel<ResponseType> response;
    }

    [Serializable]
    public class ResponseModel<ResponseType>
    {
        public string responseCode = "fail";
        public ResponseType response;
        public string responseMessage = "잘못된 요청입니다.";
    }

}