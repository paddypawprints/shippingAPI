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
using com.pb.shippingapi.model;

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
        public Token Token {get;set;}
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
        public static void URLAdd( StringBuilder url, string s)
        {
            // This is a cheerful encoder. Replace when the .net core framework URL Encoder is available.
            var bytes = Encoding.UTF8.GetBytes(s);

            foreach( var b in bytes )
            {
                switch(Convert.ToChar(b))
                {
                    case ' ': url.Append("%20");break;
                    case '!': url.Append("%21");break;
                    case '#': url.Append("%23");break;
                    case '$': url.Append("%24");break;
                    case '%': url.Append("%25");break;
                    case '&': url.Append("%26");break;
                    case '\'': url.Append("%27");break;
                    case '(': url.Append("%28");break;
                    case ')': url.Append("%29");break;
                    case '*': url.Append("%2A");break;
                    case '+': url.Append("%2B");break;
                    case ',': url.Append("%2C");break;
                    case '/': url.Append("%2F");break;
                    case ':': url.Append("%3A");break;
                    case ';': url.Append("%3B");break;
                    case '=': url.Append("%3D");break;
                    case '?': url.Append("%3F");break;
                    case '@': url.Append("%40");break;
                    case '[': url.Append("%5B");break;
                    case ']': url.Append("%5D");break;
                    default:  url.Append(Convert.ToChar(b)); break;
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

                        URLAdd( uri, ((ShippingAPIResourceAttribute)attribute).Name);
                        if (!((ShippingAPIResourceAttribute)attribute).AddId ) return;
                        uri.Append('/');
                        URLAdd( uri, fieldInfo.GetValue(request).ToString());
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
                        URLAdd( uri, ((ShippingAPIQueryAttribute)attribute).Name);
                        uri.Append('=');
                        URLAdd( uri, fieldInfo.GetValue(request).ToString());
                    }
                }
            }
            return;
        }
        internal async static Task<ShippingAPIResponse<Response>>  Post<Response, Request>(Request request)
        {
            return await HttpRequest<Response, Request>( "POST", request);
        }
        internal async static Task<ShippingAPIResponse<Response>>  Put<Response, Request>(Request request)
        {
           return await HttpRequest<Response, Request>( "PUT", request);
 
        }
        internal async static Task<ShippingAPIResponse<Response>>  Get<Response, Request>(Request request)
        {
           return await HttpRequest<Response, Request>( "GET", request);
 
        }
        internal async static Task<ShippingAPIResponse<Response>> Delete<Response, Request>(Request request)
        {
           return await HttpRequest<Response, Request>( "DELETE", request);
        }

        private async static Task<ShippingAPIResponse<Response>> HttpRequest<Response, Request>(string verb, Request request)
        {
            ShippingAPIEnvironment.Client.BaseAddress = new Uri(ShippingAPIEnvironment.ShipmentURI());
            ShippingAPIEnvironment.Client.DefaultRequestHeaders.Accept.Clear();
            ShippingAPIEnvironment.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ShippingAPIEnvironment.Client.DefaultRequestHeaders.Add("User-Agent", "Ps API Client Proxy");

            AddRequestHeaders<Request>(ShippingAPIEnvironment.Client, request);
            StringBuilder uri = new StringBuilder(ShippingAPIEnvironment.ShipmentURI());
            AddRequestQuery<Request>(uri, request);
            
            HttpResponseMessage httpResponseMessage;

            switch( verb )
            {
                case "PUT":
                    using ( var reqContent = new StringContent(JsonConvert.SerializeObject(request)) )
                    {
                        httpResponseMessage =  await ShippingAPIEnvironment.Client.PutAsync(uri.ToString(),reqContent);
                    }
                break;
                 case "POST":
                    using ( var reqContent = new StringContent(JsonConvert.SerializeObject(request)) )
                    {
                        httpResponseMessage = await ShippingAPIEnvironment.Client.PostAsync(uri.ToString(),reqContent);
                    }
                break;
                case "DELETE":
                    httpResponseMessage = await ShippingAPIEnvironment.Client.DeleteAsync(uri.ToString()); 
                    break;
                case "GET":
                    httpResponseMessage = await ShippingAPIEnvironment.Client.GetAsync(uri.ToString());  
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