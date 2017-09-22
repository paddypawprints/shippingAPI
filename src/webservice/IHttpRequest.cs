using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PitneyBowes.Developer.ShippingApi
{
    public enum HttpVerb
    {
        POST, PUT, GET, DELETE
    }

    public interface IHttpRequest
    {
         Task<ShippingApiResponse<Response>> HttpRequest<Response, Request>(string resource, HttpVerb verb, Request request, Session session = null) where Request : IShippingApiRequest;
    }
}
