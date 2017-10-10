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
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace PitneyBowes.Developer.ShippingApi
{
    public class ShippingApiResponse<Response>
    {
        public void ProcessResponseAttribute(string propName, IEnumerable<string> values)
        {
            var propertyInfo = this.GetType().GetProperty(propName);
            if (propertyInfo == null) return;
            foreach (object attribute in propertyInfo.GetCustomAttributes(true))
            {
                if (attribute is ShippingApiHeaderAttribute)
                {
                    var sa = attribute as ShippingApiHeaderAttribute;
                    var v = new StringBuilder();
                    bool firstValue = true;
                    foreach (var value in values)
                    {
                        if (!firstValue) { firstValue = false; v.Append(','); }
                        v.Append(value);
                    }
                    propertyInfo.SetValue(this, v.ToString());
                }
            }
        }
        public static explicit operator Response(ShippingApiResponse<Response> r)
        {
            return r.APIResponse;
        }
        public HttpStatusCode HttpStatus;
        public bool Success = false;
        public List<ErrorDetail> Errors = new List<ErrorDetail>();
        public Response APIResponse = default(Response);


        private static void DeserializationError(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs e)
        {
            throw new JsonSerializationException("Error deserializing", e.ErrorContext.Error);
        }

        public static void Deserialize(ISession session, RecordingStream respStream, ShippingApiResponse<Response> apiResponse, long streamPos = 0)
        {
            var deserializer = new JsonSerializer();
            deserializer.Error += DeserializationError;
            deserializer.ContractResolver = new ShippingApiContractResolver();
            ((ShippingApiContractResolver)deserializer.ContractResolver).Registry = session.SerializationRegistry;
            var recording = respStream.IsRecording;
            respStream.IsRecording = false;
            respStream.Seek(streamPos, SeekOrigin.Begin);
            JsonConverter converter = new ShippingApiResponseTypeConverter<Response>();
            Type t = (Type)converter.ReadJson(new JsonTextReader(new StreamReader(respStream)), typeof(Type), null, deserializer);
            respStream.Seek(streamPos, SeekOrigin.Begin);
            respStream.IsRecording = recording;
            if (t == typeof(ErrorFormat1))
            {
                var error = (ErrorFormat1[])deserializer.Deserialize(new StreamReader(respStream), typeof(ErrorFormat1[]));
                foreach (var e in error)
                {
                    apiResponse.Errors.Add(new ErrorDetail() { ErrorCode = e.ErrorCode, Message = e.Message, AdditionalInfo = e.AdditionalInfo });
                }
                apiResponse.APIResponse = default(Response);
            }
            else if (t == typeof(ErrorFormat2))
            {
                var error = (ErrorFormat2)deserializer.Deserialize(new StreamReader(respStream), typeof(ErrorFormat2));
                foreach (var e in error.Errors)
                {
                    apiResponse.Errors.Add(new ErrorDetail() { ErrorCode = e.ErrorCode, Message = e.ErrorDescription, AdditionalInfo = string.Empty });
                }
                apiResponse.APIResponse = default(Response);
            }
            else if (t == typeof(ErrorFormat3))
            {
                var error = (ErrorFormat3[])deserializer.Deserialize(new StreamReader(respStream), typeof(ErrorFormat3[]));
                foreach (var e in error)
                {
                    apiResponse.Errors.Add(new ErrorDetail() { ErrorCode = e.Key, Message = e.Message, AdditionalInfo = string.Empty });
                }
                apiResponse.APIResponse = default(Response);
            }
            else
            {
                apiResponse.APIResponse = (Response)deserializer.Deserialize(new StreamReader(respStream), t);
            }
        }
    }
}