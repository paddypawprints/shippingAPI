/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

*/

using System;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PitneyBowes.Developer.ShippingApi
{
    public class ShippingApiHttpRequest : IHttpRequest
    {
        internal static void AddRequestHeaders(HttpClient client, ShippingApiHeaderAttribute attribute, string propValue, string propName)
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

        public async Task<ShippingApiResponse<Response>> HttpRequest<Response, Request>(string resource, HttpVerb verb, Request request, bool deleteBody = false, ISession session = null) where Request : IShippingApiRequest
        {
            return await HttpRequestStatic<Response, Request>(resource, verb, request, deleteBody, session);
        }
        internal async static Task<ShippingApiResponse<Response>> HttpRequestStatic<Response, Request>(string resource, HttpVerb verb, Request request, bool deleteBody = false, ISession session = null) where Request : IShippingApiRequest
        {
            if (session == null) session = Globals.DefaultSession;
            var client = Globals.Client(session.EndPoint);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("user-agent", session.UserAgent);
            foreach (var h in request.GetHeaders())
            {
                AddRequestHeaders(client, h.Item1, h.Item2, h.Item3);
            }

            string uriBuilder = request.GetUri(resource);

            HttpResponseMessage httpResponseMessage;

            if (verb == HttpVerb.PUT || verb == HttpVerb.POST || (verb == HttpVerb.DELETE && deleteBody))
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
                        {
                            httpResponseMessage = await client.PutAsync(uriBuilder, reqContent);
                        }
                        else if (verb == HttpVerb.DELETE)
                        {
                            HttpRequestMessage deleteReq = new HttpRequestMessage
                            {
                                Content = reqContent,
                                Method = HttpMethod.Delete,
                                RequestUri = new Uri(client.BaseAddress + uriBuilder)
                            };
                            httpResponseMessage = await client.SendAsync(deleteReq);
                        }
                        else
                        {
                            httpResponseMessage = await client.PostAsync(uriBuilder, reqContent);
                        }
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
                using (var recordingStream = new RecordingStream(respStream, request.RecordingFullPath(resource, session), FileMode.Create))
                {
                    if (session.Record)
                    {
                        recordingStream.OpenRecord();
                    }

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        var apiResponse = new ShippingApiResponse<Response> { HttpStatus = httpResponseMessage.StatusCode, Success = httpResponseMessage.IsSuccessStatusCode };
                        recordingStream.IsRecording = true;
                        var sb = new StringBuilder();
                        foreach (var h in httpResponseMessage.Headers)
                        {
                            apiResponse.ProcessResponseAttribute(h.Key, h.Value);
                            foreach( var s in h.Value )
                            {
                                sb.Append(s);
                            }
                            recordingStream.WriteRecordCRLF(string.Format("{0}:{1}", h.Key, sb.ToString()));
                            sb.Clear();
                        }
                        recordingStream.WriteRecordCRLF("");
                        ShippingApiResponse<Response>.Deserialize(session, recordingStream, apiResponse);
                        return apiResponse;
                    }
                    else
                    {
                        var apiResponse = new ShippingApiResponse<Response> { HttpStatus = httpResponseMessage.StatusCode, Success = httpResponseMessage.IsSuccessStatusCode };
                        recordingStream.IsRecording = true;
                        recordingStream.WriteRecordCRLF("");
                        try
                        {
                            ShippingApiResponse<Response>.Deserialize(session, recordingStream, apiResponse);
                        }
                        catch (JsonException)
                        {
                            session.LogWarning(String.Format("http {0} request to {1} failed to deserialize with error {2}", verb.ToString(), uriBuilder, httpResponseMessage.StatusCode));
                            apiResponse.Errors.Add(new ErrorDetail() { ErrorCode = "HTTP " + httpResponseMessage.Version + " " + httpResponseMessage.StatusCode.ToString(), Message = httpResponseMessage.ReasonPhrase });
                        }
                        return apiResponse;
                    }
                }
            }
        }

    }
}