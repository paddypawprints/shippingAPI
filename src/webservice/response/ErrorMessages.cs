using System;
using Newtonsoft.Json;

namespace PitneyBowes.Developer.ShippingApi
{
    internal class ErrorFormat1
    {
        [JsonProperty("errorCode")]
        public string ErrorCode {get;set;}
        [JsonProperty("message")]
        public string Message {get;set;}
        [JsonProperty("addionalInfo")]
        public string AdditionalInfo { get; set;}
    }

    internal class ErrorFormat2
    {
        public class ErrorDetail
        {
            [JsonProperty("errorCode")]
            public string ErrorCode {get;set;}
            [JsonProperty("errorDescription")]
            public string ErrorDescription {get;set;}
        }
        [JsonProperty("errors")]
        public ErrorDetail[] Errors {get;set;}
    }
    internal class ErrorFormat3
    {
        [JsonProperty("key")]
        public string Key {get;set;}
        [JsonProperty("message")]
        public string Message {get;set;}

    }

}