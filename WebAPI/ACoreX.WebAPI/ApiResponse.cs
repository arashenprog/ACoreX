using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ACoreX.WebAPI
{
    [DataContract]
    public class ApiResponse
    {

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Message { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public ApiError ResponseException { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public object Result { get; set; }

        public ApiResponse(object result = null, int statusCode = 200, string message = null, ApiError apiError = null)
        {
            this.StatusCode = statusCode;
            this.Message = message;
            this.Result = result;
            this.ResponseException = apiError;
        }
    }
}
