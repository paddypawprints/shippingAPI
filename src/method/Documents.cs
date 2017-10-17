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
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;

namespace PitneyBowes.Developer.ShippingApi
{
    public static class DocumentsMethods
    {
        public static async Task WriteToStream(IDocument document, Stream stream, Func<Stream,int, Stream> nextPageAction = null, bool disposeStream = false, ISession session = null )
        {
            if (document.ContentType == ContentType.BASE64)
            { 
                int pageCount = 0;
                try
                {
                    foreach (var page in document.Pages)
                    {
                        pageCount++;
                        if (nextPageAction != null)
                        {
                            stream = nextPageAction(stream, pageCount);
                        }
                        await WriteBase64Page(page.Contents, stream, session);
                    }
                }
                finally
                {
                    if (stream != null && disposeStream) stream.Dispose();
                }
                
            }
            else
            {
                await WriteURL(document.Contents, stream, document.FileFormat, session);
            }
        }

        public static async Task WriteBase64Page(string page, Stream stream, ISession session )
        {
            var buffer = Convert.FromBase64String(page);
            await stream.WriteAsync(buffer, 0, buffer.Length);
            await stream.FlushAsync();
        }
        public static async Task WriteURL(string page, Stream stream, FileFormat format, ISession session)
        {
            var uri = new Uri(page);
            var client = Globals.Client(uri.GetComponents(UriComponents.SchemeAndServer,UriFormat.Unescaped));
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Clear();
            switch(format)
            {
                case FileFormat.PDF:
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));
                    break;
                case FileFormat.PNG:
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/png"));
                    break;
                case FileFormat.ZPL2:
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
                    break;
                default:
                    break;
            }
            client.DefaultRequestHeaders.Add("user-agent", session.UserAgent);
            var httpResponseMessage = await client.GetAsync(page);
            await httpResponseMessage.Content.CopyToAsync(stream);
            await stream.FlushAsync();
        }

    }
}
