using Newtonsoft.Json;
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
        public object Items { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int TotalCount { get; set; }

        [DataMember(EmitDefaultValue = false)]
        [JsonProperty("results")]
        public object Results { get; set; }


        public ApiResponse(object items = null, int statusCode = 200, string message = null, ApiError apiError = null, int totalCount = 0)
        {
            this.StatusCode = statusCode;
            this.Message = message;
            this.Results = new { items, totalCount };
            this.ResponseException = apiError;
        }
    }
}
   
