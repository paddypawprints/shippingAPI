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

using System.Text;
using System.Threading.Tasks;
using PitneyBowes.Developer.ShippingApi.Json;

namespace PitneyBowes.Developer.ShippingApi.Method
{

    public class RetryManifestRequest : ShippingApiRequest
    {
        public override string ContentType { get => "application/json"; }

        [ShippingApiHeaderAttribute("Bearer")]
        public override StringBuilder Authorization { get; set; }

        [ShippingApiHeaderAttribute("X-PB-TransactionId")]
        public string TransactionId { get; set; }

        [ShippingApiQuery("originalTransactionId")]
        public string OriginalTransactionId { get; set; }
    }

    public class ReprintManifestRequest : ShippingApiRequest
    {
        public override string ContentType { get => "application/json";  }

        [ShippingApiHeaderAttribute("Bearer")]
        public override StringBuilder Authorization { get; set; }

        public string ManifestId { get; set; }
    }
    public static class ManifestMethods
    {
        public async static Task<ShippingApiResponse<T>> Create<T>(T request, ISession session = null) where T : IManifest, new()
        {
            var manifestRequest = new JsonManifest<T>(request);
            return await WebMethod.Post<T, JsonManifest<T>>("/shippingservices/v1/manifests", manifestRequest, session);
        }
        public async static Task<ShippingApiResponse<T>> Reprint<T>(ReprintManifestRequest request, ISession session = null) where T : IManifest, new()
        {
            return await WebMethod.Post<T, ReprintManifestRequest> ("/shippingservices/v1/manifests/{ManifestId} ", request, session);
        }
        public async static Task<ShippingApiResponse<T>> Retry<T>(RetryManifestRequest request, ISession session = null) where T : IManifest, new()
        {
            return await WebMethod.Post<T, RetryManifestRequest>("/shippingservices/v1/manifests", request, session);
        }

    }
}
