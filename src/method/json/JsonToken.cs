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
    public class JsonToken<T> : JsonWrapper<T>, IToken where T:IToken, new()
    {
        public JsonToken() : base() { }

        public JsonToken(T t) : base(t) { }

        [JsonProperty(PropertyName="access_token")]
        public string AccessToken
        { 
            get => Wrapped.AccessToken;
            set { Wrapped.AccessToken = value; }
        }
        [JsonProperty(PropertyName="tokenType")]
        public string TokenType
        {
            get => Wrapped.TokenType;
            set { Wrapped.TokenType = value; }
        }
        [JsonProperty(PropertyName="issuedAt")]
        [JsonConverter(typeof(UnixMillisecondsTimeConverter))]
        public DateTimeOffset IssuedAt
        {
            get => Wrapped.IssuedAt;
            set { Wrapped.IssuedAt = value; }
        }
        [JsonProperty(PropertyName="expiresIn")]
        public long ExpiresIn
        {
            get => Wrapped.ExpiresIn;
            set { Wrapped.ExpiresIn = value; }
        }
        [JsonProperty(PropertyName="clientID")]
        public string ClientID
        {
            get =>Wrapped.ClientID;
            set { Wrapped.ClientID = value; }
        }
        [JsonProperty(PropertyName="org")]
        public string Org
        {
            get => Wrapped.Org;
            set { Wrapped.Org = value; }
        }
    }
 }