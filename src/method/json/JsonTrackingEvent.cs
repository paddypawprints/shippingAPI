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
using Newtonsoft.Json;

namespace PitneyBowes.Developer.ShippingApi.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class JsonTrackingEvent<T> : JsonWrapper<T>, ITrackingEvent where T : ITrackingEvent, new()
    {
        public JsonTrackingEvent() : base() { }
        public JsonTrackingEvent(T t) : base(t) { }

        [JsonProperty("eventDateTime")]
        public DateTimeOffset EventDateTime
        {
            get => Wrapped.EventDateTime;
            set { Wrapped.EventDateTime = value; }
        }
        [JsonProperty("eventCity")]
        public string EventCity
        {
            get => Wrapped.EventCity;
            set { Wrapped.EventCity = value; }
        }
        [JsonProperty("eventState")]
        public string EventState
        {
            get => Wrapped.EventState;
            set { Wrapped.EventState = value; }
        }
        [JsonProperty("postalCode")]
        public string PostalCode
        {
            get => Wrapped.PostalCode;
            set { Wrapped.PostalCode = value; }
        }
        [JsonProperty("country")]
        public string Country
        {
            get => Wrapped.Country;
            set { Wrapped.Country = value; }
        }
        [JsonProperty("scanType")]
        public string ScanType
        {
            get => Wrapped.ScanType;
            set { Wrapped.ScanType = value; }
        }
        [JsonProperty("scanDescription")]
        public string ScanDescription
        {
            get => Wrapped.ScanDescription;
            set { Wrapped.ScanDescription = value; }
        }
        [JsonProperty("packageStatus")]
        public string PackageStatus
        {
            get => Wrapped.PackageStatus;
            set { Wrapped.PackageStatus = value; }
        }
    }
}
