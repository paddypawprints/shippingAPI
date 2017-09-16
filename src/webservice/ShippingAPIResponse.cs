using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.IO;
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
            throw e.ErrorContext.Error; //TODO:
        }


        public static void Deserialize(ShippingApi.Session session, Stream respStream, ShippingApiResponse<Response> apiResponse)
        {
            var deserializer = new JsonSerializer();
            deserializer.Error += DeserializationError;
            deserializer.ContractResolver = new ShippingApiContractResolver();
            ((ShippingApiContractResolver)deserializer.ContractResolver).Session = session;

            JsonConverter converter = new ShippingApiResponseTypeConverter<Response>();
            Type t = (Type)converter.ReadJson(new JsonTextReader(new StreamReader(respStream)), typeof(Type), null, deserializer);
            respStream.Seek(0, SeekOrigin.Begin);
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
                if (session.TraceSerialization) deserializer.TraceWriter = session.NewtonSoftTrace;
                apiResponse.APIResponse = (Response)deserializer.Deserialize(new StreamReader(respStream), t);
            }
        }
    }
}