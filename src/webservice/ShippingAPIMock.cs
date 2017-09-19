using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace PitneyBowes.Developer.ShippingApi
{
    public class ShippingAPIMock : IHttpRequest
    {
        public ShippingAPIMock( string dirname = null )
        {
            Dirname = dirname;
        }

        public string Dirname { get; set; }

        public async Task<ShippingApiResponse<Response>> HttpRequest<Response, Request>(string resource, HttpVerb verb, Request request, ShippingApi.Session session = null) where Request : IShippingApiRequest
        {
            if (Dirname == null) Dirname = @"\development\shippingApi\mock";
            string uriBuilder = request.GetUri(resource);

            string fullPath = (Dirname + uriBuilder.ToLower() + @"\" + verb).Replace('?', '\\').Replace('&', '\\').Replace('/', '\\').Replace('=', '-');
            string fileName = "default";

            if (session == null) session = ShippingApi.DefaultSession;

            foreach (var h in request.GetHeaders())
            {
                if ( h.Item3.ToLower().Equals("authorization"))
                {
                    if (fileName.Equals("default"))
                    {
                        fileName = h.Item2.Substring(0,8).ToLower();
                    }
                }
                if ( h.Item1.Name.ToLower().Equals("x-pb-transactionid"))
                {
                    fileName = h.Item2.ToLower();
                }
            }
            fileName += ".http";

            if ( File.Exists(fullPath + '\\' + fileName))
            {
                var apiResponse = new ShippingApiResponse<Response> { HttpStatus = HttpStatusCode.OK, Success = true };
                long jsonPosition = 0;
                using (var fileStream = new FileStream(fullPath + '\\' + fileName, FileMode.Open, FileAccess.Read))
                using (var fileReader = new StreamReader(fileStream))
                {
                    string line;
                    while ( true )
                    {
                        line = await fileReader.ReadLineAsync();
                        jsonPosition += line.Length + 2; // + CRLF
                        if (line == String.Empty) break;
                        var header = line.Split(':');
                        if (header.Length == 2 )
                            apiResponse.ProcessResponseAttribute(header[0].Trim(), header[1].Trim().Split(','));
                    }
                }
                using (var fileStream = new FileStream(fullPath + '\\' + fileName, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        ShippingApiResponse<Response>.Deserialize(session, fileStream, apiResponse, jsonPosition);
                    }
                    catch (Exception ex)
                    {
                        session.LogError(string.Format("Mock request {0} got deserialization exception {1}", uriBuilder, ex.Message));
                        throw ex;
                    }
                }
                return apiResponse;


            }

            else
            {
                var apiResponse = new ShippingApiResponse<Response> { HttpStatus = HttpStatusCode.NotFound, Success = false };
                session.LogDebug(string.Format("Mock request failed {0}, {1}", uriBuilder, fullPath + "\\" + fileName));
                apiResponse.Errors.Add(new ErrorDetail() { ErrorCode = "Mock 401", Message = "Could not find response file" + fullPath + "\\" + fileName });
                return apiResponse;

            }
        }

    }
}

