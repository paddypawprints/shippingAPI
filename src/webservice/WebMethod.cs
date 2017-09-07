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

namespace PitneyBowes.Developer.ShippingApi
{
    public class ErrorDetail
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public string AdditionalInfo { get; set; }

    }

    public class ShippingAPIResponse<Response>
    {
        public static explicit operator Response( ShippingAPIResponse<Response> r )
        {
            return r.APIResponse;
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

        private static void SerializeBody<Request>( StreamWriter writer, Request request, ShippingApi.Session session ) where Request : IShippingApiRequest
        {
            switch (request.ContentType)
            {
                case "application/json":
                    var serializer = new JsonSerializer(){ ContractResolver = new ShippingAPIContractResolver() };
                    ((ShippingAPIContractResolver)serializer.ContractResolver).Session = session;
                    serializer.NullValueHandling = NullValueHandling.Ignore;
                    serializer.Formatting = Formatting.Indented;
                    if (session.TraceSerialization) serializer.TraceWriter = session.NewtonSoftTrace;
                    serializer.Serialize(writer, request);
                    writer.Flush();
                    return;

                case "application/x-www-form-urlencoded":
                    SerializeAsFormPost<Request>(writer, request);
                    return;
                default:
                    throw new Exception(); //TODO:
            }
        }

        private static void SerializeAsFormPost<Request>( StreamWriter writer, Request request ) 
        {
            if (request == null ) throw new Exception(); //TODO:

            bool isFirst = true;
                
            foreach( var propertyInfo in typeof(Request).GetProperties())
            {
                foreach (object attribute in propertyInfo.GetCustomAttributes(true))
                {
                    if (attribute is JsonPropertyAttribute )
                    {
                        if (!isFirst) { writer.WriteLine(); isFirst = false; }
                        writer.Write(((JsonPropertyAttribute)attribute).PropertyName);
                        writer.Write('=');
                        writer.Write((string) propertyInfo.GetValue(request));
                    }
                }
            }
            writer.Flush();
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
                            if (propertyInfo.GetValue(request)==null)
                            {
                                v = null;
                            }
                            else
                            {
                                var val = propertyInfo.GetValue(request) as string;
                                v = val ?? propertyInfo.GetValue(request).ToString();
                            }
                        }
                        propAction( (Attribute)attribute, ((Attribute)attribute).Name, v, propertyInfo.Name);
                    }
                }
            }
        }
        private static void ProcessResponseAttributes<Response>(Response response, HttpHeaders headers) 
        {
            if (response == null) return;

            foreach (var propertyInfo in typeof(Response).GetProperties())
            {

                foreach (object attribute in propertyInfo.GetCustomAttributes(true))
                {
                    if (attribute is ShippingAPIHeaderAttribute)
                    {
                        var sa = attribute as ShippingAPIHeaderAttribute;
                        var v = new StringBuilder();
                        bool firstValue = true;
                        foreach( var value in headers.GetValues(sa.Name))
                        {
                            if (!firstValue) { firstValue = false; v.Append(','); }
                            v.Append(value);
                        }
                        propertyInfo.SetValue(response, v.ToString());
                    }
                }
            }
        }

        internal static void AddRequestHeaders<RequestHeader>( HttpClient client, RequestHeader request)
        {
            ProcessRequestAttributes<RequestHeader,ShippingAPIHeaderAttribute>(request, 
                (a,s,v,p)=> {
                    if (a.OmitIfEmpty && (v == null || v.Equals(String.Empty))) return;
                    switch (p)
                    {
                        case "Authorization": 
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(s, v);
                            break;
                        default:
                            client.DefaultRequestHeaders.Remove(s);
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
                    if (a.AddId)
                    {
                        uri.Append('/');
                        UrlHelper.URLAdd(uri, (string)v);
                    }
                    if ( a.PathSuffix != null )
                    {
                        UrlHelper.URLAdd(uri, (string)v);
                    }
                }
            );   
         }
        internal static void AddRequestQuery<RequestQuery>( StringBuilder uri, RequestQuery request)
        {
            if (request == null) return;

            bool hasQuery = false;
 
             ProcessRequestAttributes<RequestQuery, ShippingAPIQueryAttribute>(request, 
                (a,s,v, p)=> {
                    if (a.OmitIfEmpty && v.Equals(String.Empty)) return;
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
                    using (var stream = new MemoryStream())
                    using (var writer = new StreamWriter(stream))
                    {
                        SerializeBody<Request>(writer, request, session);
                        stream.Seek(0, SeekOrigin.Begin);
                        using (var reqContent = new StreamContent(stream))
                        {
                            httpResponseMessage = await client.PutAsync(resource.ToString(), reqContent);
                        }
                    }
                break;
                 case HttpVerb.POST:
                    using (var stream = new MemoryStream())
                    using (var writer = new StreamWriter(stream))
                    {
                        SerializeBody<Request>(writer, request, session);
                        stream.Seek(0, SeekOrigin.Begin);
                        using (var reqContent = new StreamContent(stream))
                        {
                            httpResponseMessage = await client.PostAsync(resource.ToString(), reqContent);
                        }
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

            var apiResponse = new ShippingAPIResponse<Response> { HttpStatus = httpResponseMessage.StatusCode, Success = httpResponseMessage.IsSuccessStatusCode };
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using (var respStream = await httpResponseMessage.Content.ReadAsStreamAsync())
                {
                    var deserializer = new JsonSerializer();
                    deserializer.Error += DeserializationError;
                    deserializer.ContractResolver = new ShippingAPIContractResolver();
                    ((ShippingAPIContractResolver)deserializer.ContractResolver).Session = session;

                    JsonConverter converter = new ShippingAPIResponseTypeConverter<Response>();
                    Type t = (Type)converter.ReadJson(new JsonTextReader(new StreamReader(respStream)), typeof(Type), null, deserializer);
                    respStream.Seek(0, SeekOrigin.Begin);
                    if (t == typeof(ErrorFormat1))
                    {
                        var error = (ErrorFormat1[])deserializer.Deserialize(new StreamReader(respStream), typeof(ErrorFormat1[]));
                        foreach( var e in error )
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
                ProcessResponseAttributes<Response>(apiResponse.APIResponse, httpResponseMessage.Headers);
            }
            else
            {
                apiResponse.Errors.Add(new ErrorDetail() { ErrorCode = "HTTP " + httpResponseMessage.Version + " " + httpResponseMessage.StatusCode.ToString(), Message = httpResponseMessage.ReasonPhrase });
            }
            return apiResponse;
        }

        static void DeserializationError(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs e)
        {
            throw e.ErrorContext.Error; //TODO:
        }

    }
}