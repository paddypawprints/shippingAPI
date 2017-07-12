using System;
using Newtonsoft.Json;

namespace com.pb.shippingapi
{
    class ErrorFormat1
    {
        [JsonProperty("errorCode")]
        string ErrorCode {get;set;}
        [JsonProperty("message")]
        string Message {get;set;}
        [JsonProperty("addionalInfo")]
       string AdditionalInfo { get; set;}
    }

    class ErrorFormat2
    {
        class ErrorDetail
        {
            [JsonProperty("errorCode")]
            string ErrorCode {get;set;}
            [JsonProperty("errorDescription")]
            string ErrorDescription {get;set;}
        }
        [JsonProperty("errors")]
        ErrorDetail[] Errors {get;set;}
    }
    class ErrorFormat3
    {
        [JsonProperty("key")]
        string Key {get;set;}
        [JsonProperty("message")]
        string Message {get;set;}

    }

}