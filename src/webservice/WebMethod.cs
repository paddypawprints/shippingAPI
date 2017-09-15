using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace PitneyBowes.Developer.ShippingApi
{
    public enum HttpVerb
    {
        POST, PUT, GET, DELETE
    }

    internal class WebMethod
    {

        internal async static Task<ShippingAPIResponse<Response>> Post<Response, Request>(string uri, Request request, ShippingApi.Session session = null) where Request : IShippingApiRequest
        {
            if (session == null) session = ShippingApi.DefaultSession;
            return await session.Requestor.HttpRequest<Response, Request>( uri, HttpVerb.POST, request, session);
        }
        internal async static Task<ShippingAPIResponse<Response>> Put<Response, Request>(string uri, Request request, ShippingApi.Session session = null) where Request : IShippingApiRequest
        {
            if (session == null) session = ShippingApi.DefaultSession;
            return await session.Requestor.HttpRequest<Response, Request>( uri, HttpVerb.PUT, request, session);
 
        }
        internal async static Task<ShippingAPIResponse<Response>> Get<Response, Request>(string uri, Request request, ShippingApi.Session session = null) where Request : IShippingApiRequest
        {
            if (session == null) session = ShippingApi.DefaultSession;
            return await session.Requestor.HttpRequest<Response, Request>( uri, HttpVerb.GET, request, session);
 
        }
        internal async static Task<ShippingAPIResponse<Response>> Delete<Response, Request>(string uri, Request request, ShippingApi.Session session = null) where Request : IShippingApiRequest
        {
            if (session == null) session = ShippingApi.DefaultSession;
            return await session.Requestor.HttpRequest<Response, Request>( uri, HttpVerb.DELETE, request, session);
        }
    }
}