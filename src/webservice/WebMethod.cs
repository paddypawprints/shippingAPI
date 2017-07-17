using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace com.pb.shippingapi
{
    public class ShippingAPIHeaderAttribute : Attribute
    {
        public string Name { get;set; }
        public ShippingAPIHeaderAttribute(string name)
        {
            Name = name;
        }

    }
    public class ShippingAPIQueryAttribute : Attribute
    {
        public string Name { get;set; }
        public ShippingAPIQueryAttribute(string name)
        {
            Name = name;
        }

    }

    public class ShippingAPIResourceAttribute : Attribute
    {
        public string Name { get;set; }
        public bool AddId { get;set; }
        public ShippingAPIResourceAttribute(string name, bool addId = true)
        {
            Name = name;
            AddId = addId;
        }

    }

    public class ShippingAPIResponse<Response>
    {
        public static explicit operator Response( ShippingAPIResponse<Response> r )
        {
            return r.APIResponse;
        }
        public class ErrorDetail
        {
                public string ErrorCode {get;set;}
                public string Message {get;set;}
                public string AdditionalInfo {get;set;}

        }   
        public HttpStatusCode HttpStatus;  
        public bool Success = false;   
        public List<ErrorDetail> Errors = new List<ErrorDetail>();
        public Response APIResponse = default(Response);

    }

    internal class WebMethod
    {
        internal static void AddRequestHeaders<RequestHeader>( HttpClient client, RequestHeader request)
        {
            if (request == null ) return;
                
            foreach( var fieldInfo in typeof(RequestHeader).GetFields())
            {
 
                foreach (object attribute in fieldInfo.GetCustomAttributes(true))
                {
                    if (attribute is ShippingAPIHeaderAttribute )
                    {
                        client.DefaultRequestHeaders.Add(((ShippingAPIHeaderAttribute)attribute).Name, fieldInfo.GetValue(request).ToString());
                    }
                }
            }
        }


        internal static void AddRequestResource<RequestQuery>( StringBuilder uri, RequestQuery request)
        {
            if (request == null) return;
 
            foreach( var fieldInfo in typeof(RequestQuery).GetFields())
            {
                foreach (object attribute in fieldInfo.GetCustomAttributes(true))
                {
                    if (attribute is ShippingAPIResourceAttribute )
                    {
                            uri.Append('/');

                        UrlHelper.URLAdd( uri, ((ShippingAPIResourceAttribute)attribute).Name);
                        if (!((ShippingAPIResourceAttribute)attribute).AddId ) return;
                        uri.Append('/');
                        UrlHelper.URLAdd( uri, fieldInfo.GetValue(request).ToString());
                    }
                }
            }
            return;
        }
        internal static void AddRequestQuery<RequestQuery>( StringBuilder uri, RequestQuery request)
        {
            if (request == null) return;

            bool hasQuery = false;
 
            foreach( var fieldInfo in typeof(RequestQuery).GetFields())
            {
 
                foreach (object attribute in fieldInfo.GetCustomAttributes(true))
                {
                    if (attribute is ShippingAPIQueryAttribute )
                    {
                        if ( !hasQuery )
                        {
                            uri.Append('?');
                            hasQuery = true;
                        }
                        else
                        {
                            uri.Append(',');
                        }
                        UrlHelper.URLAdd( uri, ((ShippingAPIQueryAttribute)attribute).Name);
                        uri.Append('=');
                        UrlHelper.URLAdd( uri, fieldInfo.GetValue(request).ToString());
                    }
                }
            }
            return;
        }

        private enum HttpVerb
        {
            POST, PUT, GET, DELETE
        }
        internal async static Task<ShippingAPIResponse<Response>>  Post<Response, Request>(string uri, Request request, ShippingApi.Session session = null)
        {
            return await HttpRequest<Response, Request>( uri, HttpVerb.POST, request, session);
        }
        internal async static Task<ShippingAPIResponse<Response>>  Put<Response, Request>(string uri, Request request, ShippingApi.Session session = null)
        {
           return await HttpRequest<Response, Request>( uri, HttpVerb.PUT, request, session);
 
        }
        internal async static Task<ShippingAPIResponse<Response>>  Get<Response, Request>(string uri, Request request, ShippingApi.Session session = null)
        {
           return await HttpRequest<Response, Request>( uri, HttpVerb.GET, request, session);
 
        }
        internal async static Task<ShippingAPIResponse<Response>> Delete<Response, Request>(string uri, Request request, ShippingApi.Session session = null)
        {
           return await HttpRequest<Response, Request>( uri, HttpVerb.DELETE, request, session);
        }

        private async static Task<ShippingAPIResponse<Response>> HttpRequest<Response, Request>(string uri, HttpVerb verb, Request request, ShippingApi.Session session = null)
        {
            if ( session == null ) session = ShippingApi.DefaultSession;
            session.Client.BaseAddress = new Uri(session.EndPoint + uri );
            session.Client.DefaultRequestHeaders.Accept.Clear();
            session.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            session.Client.DefaultRequestHeaders.Add("User-Agent", "Ps API Client Proxy");

            AddRequestHeaders<Request>(session.Client, request);
            StringBuilder uriBuilder = new StringBuilder(session.EndPoint + uri);
            AddRequestQuery<Request>(uriBuilder, request);
            
            HttpResponseMessage httpResponseMessage;

            switch( verb )
            {
                case HttpVerb.PUT:
                    using ( var reqContent = new StringContent(JsonConvert.SerializeObject(request)) )
                    {
                        httpResponseMessage =  await session.Client.PutAsync(uri.ToString(),reqContent);
                    }
                break;
                 case HttpVerb.POST:
                    using ( var reqContent = new StringContent(JsonConvert.SerializeObject(request)) )
                    {
                        httpResponseMessage = await session.Client.PostAsync(uri.ToString(),reqContent);
                    }
                break;
                case HttpVerb.DELETE:
                    httpResponseMessage = await session.Client.DeleteAsync(uri.ToString()); 
                    break;
                case HttpVerb.GET:
                    httpResponseMessage = await session.Client.GetAsync(uri.ToString());  
                    break;   
                default:
                    throw new ArgumentException(); //TODO
            }

            if ( httpResponseMessage.StatusCode == HttpStatusCode.OK )
            {
                Response resp;
                using ( var respStream =  await httpResponseMessage.Content.ReadAsStreamAsync())
                {
                    var deserializer = new JsonSerializer();
                    var apiReader = new ShippingAPIJsonReader(new StreamReader(respStream));
                    JsonConverter converter = new ShippingAPIResponseTypeConverter<Response>();
                    Type t = (Type)converter.ReadJson(apiReader,typeof(Type), null, deserializer);
                    resp = (Response)deserializer.Deserialize(apiReader, t);
                }
                return new ShippingAPIResponse<Response> { HttpStatus = httpResponseMessage.StatusCode,APIResponse = resp };
            }
            else
                return new ShippingAPIResponse<Response>() { HttpStatus = httpResponseMessage.StatusCode, APIResponse = default(Response) };
        }
    }
}