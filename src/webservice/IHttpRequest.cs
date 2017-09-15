using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PitneyBowes.Developer.ShippingApi
{
    interface IHttpRequest
    {
         Task<ShippingAPIResponse<Response>> HttpRequest<Response, Request>(string resource, HttpVerb verb, Request request, ShippingApi.Session session = null) where Request : IShippingApiRequest;
    }
}
