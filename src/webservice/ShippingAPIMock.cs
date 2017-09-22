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
            string cwd = Directory.GetCurrentDirectory();

            if ( File.Exists(fullPath))
            {
                var apiResponse = new ShippingApiResponse<Response> { HttpStatus = HttpStatusCode.OK, Success = true };
                long jsonPosition = 0;
                using (var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                using (var fileReader = new StreamReader(fileStream))
                {

                    for (var line = await fileReader.ReadLineAsync(); line!=string.Empty; line = await fileReader.ReadLineAsync())
                    {
                        jsonPosition += line.Length + 2; // + CRLF
                        if (line.IndexOf(':') == -1) continue;
                        var headerName = line.Substring(0, line.IndexOf(':'));
                        var headerValue = line.IndexOf(':') == line.Length? string.Empty : line.Substring( line.IndexOf(':')+1 );
                        apiResponse.ProcessResponseAttribute(headerName, headerValue.Split(','));
                    }
                }
                using (var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var recordingStream = new RecordingStream(fileStream, request.RecordingFullPath(resource, session), FileMode.Create))
                {
                    try
                    {
                        //dont open the record file
                        ShippingApiResponse<Response>.Deserialize(session, recordingStream, apiResponse, jsonPosition);
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

