using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace PitneyBowes.Developer.ShippingApi
{
    internal enum HttpVerb
    {
        POST, PUT, GET, DELETE
    }

    internal class WebMethod
    {

        internal async static Task<ShippingApiResponse<Response>> Post<Response, Request>(string uri, Request request, ShippingApi.Session session = null) where Request : IShippingApiRequest
        {
            if (session == null) session = ShippingApi.DefaultSession;
            return await session.Requestor.HttpRequest<Response, Request>( uri, HttpVerb.POST, request, session);
        }
        internal async static Task<ShippingApiResponse<Response>> Put<Response, Request>(string uri, Request request, ShippingApi.Session session = null) where Request : IShippingApiRequest
        {
            if (session == null) session = ShippingApi.DefaultSession;
            return await session.Requestor.HttpRequest<Response, Request>( uri, HttpVerb.PUT, request, session);
 
        }
        internal async static Task<ShippingApiResponse<Response>> Get<Response, Request>(string uri, Request request, ShippingApi.Session session = null) where Request : IShippingApiRequest
        {
            if (session == null) session = ShippingApi.DefaultSession;
            return await session.Requestor.HttpRequest<Response, Request>( uri, HttpVerb.GET, request, session);
 
        }
        internal async static Task<ShippingApiResponse<Response>> Delete<Response, Request>(string uri, Request request, ShippingApi.Session session = null) where Request : IShippingApiRequest
        {
            if (session == null) session = ShippingApi.DefaultSession;
            return await session.Requestor.HttpRequest<Response, Request>( uri, HttpVerb.DELETE, request, session);
        }
    }
}