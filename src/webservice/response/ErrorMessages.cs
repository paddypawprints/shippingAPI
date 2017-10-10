/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

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