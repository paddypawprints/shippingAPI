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
    public class ShippingAPIAttribute : Attribute
    {
        public string Name { get;set; }
        public ShippingAPIAttribute(string name)
        {
            Name = name;
        }

    }

    public class ShippingAPIHeaderAttribute : ShippingAPIAttribute
    {
        public ShippingAPIHeaderAttribute(string name) : base(name)
        {
        }

    }
    public class ShippingAPIQueryAttribute : ShippingAPIAttribute
    {
        public ShippingAPIQueryAttribute(string name) :base(name)
        {
        }

    }

    public class ShippingAPIResourceAttribute : ShippingAPIAttribute
    {
        public bool AddId { get;set; }
        public ShippingAPIResourceAttribute(string name, bool addId = true) : base(name)
        {
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

    public interface IShippingApiRequest
    {
        string ContentType {get;set;}
        StringBuilder Authorization {get;set;}
    }
    internal class WebMethod
    {

        private static string SerializeBody<Request>( Request request ) where Request : IShippingApiRequest
        {
            switch(request.ContentType)
            {
                case "application/json":
                    return JsonConvert.SerializeObject(request);
                case "application/x-www-form-urlencoded":
                    return SerializeAsFormPost<Request>(request);
                default:
                    throw new Exception(); //TODO:
            }
        }

        private static string SerializeAsFormPost<Request>( Request request ) 
        {
            // assume JSON serializer opt in is set
            if (request == null ) throw new Exception(); //TODO:

            var body = new StringBuilder();
            bool isFirst = true;
                
            foreach( var propertyInfo in typeof(Request).GetProperties())
            {
                foreach (object attribute in propertyInfo.GetCustomAttributes(true))
                {
                    if (attribute is JsonPropertyAttribute )
                    {
                        if (!isFirst) { body.AppendLine(); isFirst = false; }
                        string name = ((JsonPropertyAttribute)attribute).PropertyName;
                        string value = (string) propertyInfo.GetValue(request);
                        body.Append( name).Append("=");
                        UrlHelper.URLAdd(body, value);
                    }
                }
            }
            return body.ToString();
        }

        private static void ProcessRequestAttributes<RequestHeader,Attribute>( RequestHeader request, Action<Attribute, string, string, string> propAction  ) where Attribute : ShippingAPIAttribute
        {
            if (request == null ) return;
                
            foreach( var propertyInfo in typeof(RequestHeader).GetProperties())
            {
 
                foreach (object attribute in propertyInfo.GetCustomAttributes(true))
                {
                    if (attribute is Attribute )
                    {
                        string v;
                        if (propertyInfo.GetValue(request) is StringBuilder)
                        {
                            v = ((StringBuilder)propertyInfo.GetValue(request)).ToString();
                        }
                        else
                        {
                            v = (string)propertyInfo.GetValue(request);
                        }
                        propAction( (Attribute)attribute, ((Attribute)attribute).Name, v, propertyInfo.Name);
                    }
                }
            }
        }

        internal static void AddRequestHeaders<RequestHeader>( HttpClient client, RequestHeader request)
        {
            ProcessRequestAttributes<RequestHeader,ShippingAPIHeaderAttribute>(request, 
                (a,s,v,p)=> {

                    switch(p)
                    {
                        case "Authorization": 
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(s, v);
                            break;
                        default:
                            client.DefaultRequestHeaders.Add(s, v );
                            break; 
                    }
                }
            );
        }
        internal static void AddRequestResource<RequestQuery>( StringBuilder uri, RequestQuery request)
        { 
             ProcessRequestAttributes<RequestQuery,ShippingAPIResourceAttribute>(request, 
                (a,s,v, p)=> {

                        uri.Append('/');
                        UrlHelper.URLAdd( uri, s);
                        if (!((ShippingAPIResourceAttribute)a).AddId ) return;
                        uri.Append('/');
                        UrlHelper.URLAdd( uri, (string)v);
                }
            );   
         }
        internal static void AddRequestQuery<RequestQuery>( StringBuilder uri, RequestQuery request)
        {
            if (request == null) return;

            bool hasQuery = false;
 
             ProcessRequestAttributes<RequestQuery,ShippingAPIResourceAttribute>(request, 
                (a,s,v, p)=> {
                        if ( !hasQuery )
                        {
                            uri.Append('?');
                            hasQuery = true;
                        }
                        else
                        {
                            uri.Append(',');
                        }
                        UrlHelper.URLAdd( uri, s);
                        uri.Append('=');
                        UrlHelper.URLAdd( uri, v);
                }
            );   
        }

        private enum HttpVerb
        {
            POST, PUT, GET, DELETE
        }
        internal async static Task<ShippingAPIResponse<Response>>  Post<Response, Request>(string uri, Request request, ShippingApi.Session session = null) where Request : IShippingApiRequest
        {
            return await HttpRequest<Response, Request>( uri, HttpVerb.POST, request, session);
        }
        internal async static Task<ShippingAPIResponse<Response>>  Put<Response, Request>(string uri, Request request, ShippingApi.Session session = null) where Request : IShippingApiRequest
        {
           return await HttpRequest<Response, Request>( uri, HttpVerb.PUT, request, session);
 
        }
        internal async static Task<ShippingAPIResponse<Response>>  Get<Response, Request>(string uri, Request request, ShippingApi.Session session = null) where Request : IShippingApiRequest
        {
           return await HttpRequest<Response, Request>( uri, HttpVerb.GET, request, session);
 
        }
        internal async static Task<ShippingAPIResponse<Response>> Delete<Response, Request>(string uri, Request request, ShippingApi.Session session = null) where Request : IShippingApiRequest
        {
           return await HttpRequest<Response, Request>( uri, HttpVerb.DELETE, request, session);
        }

        private async static Task<ShippingAPIResponse<Response>> HttpRequest<Response, Request>(string resource, HttpVerb verb, Request request, ShippingApi.Session session = null) where Request : IShippingApiRequest
        {
            if ( session == null ) session = ShippingApi.DefaultSession;
            var client = session.Client(session.EndPoint );
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(request.ContentType));
            client.DefaultRequestHeaders.Add("User-Agent", "Ps API Client Proxy");
            AddRequestHeaders<Request>(client, request);

            StringBuilder uriBuilder = new StringBuilder(session.EndPoint + resource);
            AddRequestQuery<Request>(uriBuilder, request);
            
            HttpResponseMessage httpResponseMessage;

            switch( verb )
            {
                case HttpVerb.PUT:
                    using ( var reqContent = new StringContent(SerializeBody<Request>(request), Encoding.UTF8, request.ContentType) )
                    {
                        httpResponseMessage =  await client.PutAsync(resource.ToString(),reqContent);
                    }
                break;
                 case HttpVerb.POST:
                    using ( var reqContent = new StringContent(SerializeBody<Request>(request), Encoding.UTF8, request.ContentType) )
                    {
                        httpResponseMessage = await client.PostAsync(resource.ToString(),reqContent);
                    }
                break;
                case HttpVerb.DELETE:
                    httpResponseMessage = await client.DeleteAsync(resource.ToString()); 
                    break;
                case HttpVerb.GET:
                    httpResponseMessage = await client.GetAsync(resource.ToString());  
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
                    JsonConverter converter = new ShippingAPIResponseTypeConverter<Response>();
                    Type t = (Type)converter.ReadJson(new JsonTextReader(new StreamReader(respStream)),typeof(Type), null, deserializer);
                    respStream.Seek(0,SeekOrigin.Begin);
                    resp = (Response)deserializer.Deserialize(new StreamReader(respStream), t);
                }
                return new ShippingAPIResponse<Response> { HttpStatus = httpResponseMessage.StatusCode,APIResponse = resp };
            }
            else
                return new ShippingAPIResponse<Response>() { HttpStatus = httpResponseMessage.StatusCode, APIResponse = default(Response) };
        }
    }
}