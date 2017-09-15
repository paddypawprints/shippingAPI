﻿using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PitneyBowes.Developer.ShippingApi
{
    internal class ShippingApiHttpRequest : IHttpRequest
    {
        internal static void AddRequestHeaders(HttpClient client, ShippingAPIHeaderAttribute attribute, string propValue, string propName)
        {
            if (attribute.OmitIfEmpty && (propValue == null || propValue.Equals(String.Empty))) return;
            switch (propName)
            {
                case "Authorization":
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(attribute.Name, propValue);
                    break;

                default:
                    client.DefaultRequestHeaders.Remove(attribute.Name);
                    client.DefaultRequestHeaders.Add(attribute.Name, propValue);
                    break;
            }
        }

        public async Task<ShippingAPIResponse<Response>> HttpRequest<Response, Request>(string resource, HttpVerb verb, Request request, ShippingApi.Session session = null) where Request : IShippingApiRequest
        {
            return await HttpRequestStatic<Response, Request>(resource, verb, request, session);
        }
        internal async static Task<ShippingAPIResponse<Response>> HttpRequestStatic<Response, Request>(string resource, HttpVerb verb, Request request, ShippingApi.Session session = null) where Request : IShippingApiRequest
        {
            if (session == null) session = ShippingApi.DefaultSession;
            var client = session.Client(session.EndPoint);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("user-agent", "Ps API Client Proxy");
            foreach (var h in request.GetHeaders())
            {
                AddRequestHeaders(client, h.Item1, h.Item2, h.Item3);
            }

            string uriBuilder = request.GetUri(resource);

            return await HttpRequest<Response, Request>(verb, request, session, client, uriBuilder);
        }

        private static async Task<ShippingAPIResponse<Response>> HttpRequest<Response, Request>(HttpVerb verb, Request request, ShippingApi.Session session, HttpClient client, string uriBuilder) where Request : IShippingApiRequest
        {
            HttpResponseMessage httpResponseMessage;

            if (verb == HttpVerb.PUT || verb == HttpVerb.POST)
            {
                using (var stream = new MemoryStream())
                using (var writer = new StreamWriter(stream))
                {
                    request.SerializeBody(writer, session);
                    stream.Seek(0, SeekOrigin.Begin);
                    using (var reqContent = new StreamContent(stream))
                    {
                        reqContent.Headers.ContentType = new MediaTypeHeaderValue(request.ContentType);
                        if (verb == HttpVerb.PUT)
                            httpResponseMessage = await client.PutAsync(uriBuilder, reqContent);
                        else
                            httpResponseMessage = await client.PostAsync(uriBuilder, reqContent);
                    }
                }
            }
            else if (verb == HttpVerb.DELETE)
            {
                httpResponseMessage = await client.DeleteAsync(uriBuilder);
            }
            else
            {
                httpResponseMessage = await client.GetAsync(uriBuilder);
            }

            using (var respStream = await httpResponseMessage.Content.ReadAsStreamAsync())
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var apiResponse = new ShippingAPIResponse<Response> { HttpStatus = httpResponseMessage.StatusCode, Success = httpResponseMessage.IsSuccessStatusCode };
                    ShippingAPIResponse<Response>.Deserialize(session, respStream, apiResponse);
                    foreach (var h in httpResponseMessage.Headers)
                    {
                        apiResponse.ProcessResponseAttribute(h.Key, h.Value);
                    }
                    return apiResponse;

                }
                else
                {
                    var apiResponse = new ShippingAPIResponse<Response> { HttpStatus = httpResponseMessage.StatusCode, Success = httpResponseMessage.IsSuccessStatusCode };

                    apiResponse.Errors.Add(new ErrorDetail() { ErrorCode = "HTTP " + httpResponseMessage.Version + " " + httpResponseMessage.StatusCode.ToString(), Message = httpResponseMessage.ReasonPhrase });
                    return apiResponse;

                }
            }
        }

    }
}