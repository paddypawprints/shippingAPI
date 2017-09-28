
using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;

namespace PitneyBowes.Developer.ShippingApi
{

    internal class Token : IToken
    {
        virtual public string AccessToken { get; set; }
        virtual public string TokenType { get; set; }
        virtual public DateTimeOffset IssuedAt { get; set; }
        virtual public long ExpiresIn { get; set; }
        virtual public string ClientID { get; set; }
        virtual public string Org { get; set; }
    }

    internal class WebMethod
    {
        private static bool Retry(HttpStatusCode status, List<ErrorDetail> errors)
        {
            foreach( var e in errors)
            {
                if (e.ErrorCode.Equals("PB-APIM-ERR-1003") || e.ErrorCode.Equals("PB-APIM-ERR-1003")) return true;
            }
            return false;
        }
        private async static Task<ShippingApiResponse<Response>> Request<Response, Request>(string uri, HttpVerb verb, Request request, Session session = null) where Request : IShippingApiRequest
        {
            if (session == null) session = SessionDefaults.DefaultSession;
            var response = new ShippingApiResponse<Response>();

            for (int retries = session.Retries;  retries > 0; retries--)
            {
                if (session.AuthToken.AccessToken == null ) //TODO: Check if token should have expired
                {
                    var tokenResponse = TokenMethods.token<Token>().GetAwaiter().GetResult();
                    if (!tokenResponse.Success)
                    {
                        session.AuthToken = null;
                        continue;
                    }
                    session.AuthToken = tokenResponse.APIResponse;
                }

                request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
                response = await session.Requester.HttpRequest<Response, Request>(uri, verb, request, session);
                if (response.Success) break;
                if (!Retry(response.HttpStatus, response.Errors)) break;
            }
            return response;
        }
        internal async static Task<ShippingApiResponse<Response>> Post<Response, Request>(string uri, Request request, Session session = null) where Request : IShippingApiRequest
        {
            return await Request<Response, Request>(uri, HttpVerb.PUT, request, session);
        }
        internal async static Task<ShippingApiResponse<Response>> Put<Response, Request>(string uri, Request request, Session session = null) where Request : IShippingApiRequest
        {
            return await Request<Response, Request>(uri, HttpVerb.PUT, request, session);
        }
        internal async static Task<ShippingApiResponse<Response>> Get<Response, Request>(string uri, Request request, Session session = null) where Request : IShippingApiRequest
        {
            return await Request<Response, Request>( uri, HttpVerb.GET, request, session); 
        }
        internal async static Task<ShippingApiResponse<Response>> Delete<Response, Request>(string uri, Request request, Session session = null) where Request : IShippingApiRequest
        {
            return await Request<Response, Request>( uri, HttpVerb.DELETE, request, session);
        }
    }
}