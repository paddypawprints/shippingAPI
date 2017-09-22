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

        public async Task<ShippingApiResponse<Response>> HttpRequest<Response, Request>(string resource, HttpVerb verb, Request request, Session session = null) where Request : IShippingApiRequest
        {
            string fullPath = request.RecordingFullPath(resource, session);

            if ( File.Exists(fullPath))
            {
                var apiResponse = new ShippingApiResponse<Response> { HttpStatus = HttpStatusCode.OK, Success = true };
                long jsonPosition = 0;
                using (var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
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
                using (var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        ShippingApiResponse<Response>.Deserialize(session, fileStream, apiResponse, jsonPosition);
                    }
                    catch (Exception ex)
                    {
                        session.LogError(string.Format("Mock request {0} got deserialization exception {1}", fullPath, ex.Message));
                        throw ex;
                    }
                }
                return apiResponse;


            }

            else
            {
                var apiResponse = new ShippingApiResponse<Response> { HttpStatus = HttpStatusCode.NotFound, Success = false };
                session.LogDebug(string.Format("Mock request failed {0}",fullPath));
                apiResponse.Errors.Add(new ErrorDetail() { ErrorCode = "Mock 401", Message = "Could not find response file" + fullPath });
                return apiResponse;

            }
        }

    }
}

