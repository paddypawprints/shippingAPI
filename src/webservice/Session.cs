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
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PitneyBowes.Developer.ShippingApi
{
    /// <summary>
    /// Various environmental items that get injected into most methods. They can be overridden to configure the system.
    /// </summary>
    public class Session : ISession
    {
        private Dictionary<string, string> _configs = new Dictionary<string, string>();

        public Session()
        {
            Record = false;
            RecordPath = Globals.GetPath(Path.GetTempPath(), "recordings", "shippingApi");
            RecordOverwrite = false;
            Retries = 3;
            _configs.Add("SANDBOX_ENDPOINT", "https://api-sandbox.pitneybowes.com");
            _configs.Add("PRODUCTION_ENDPOINT", "https://api-sandbox.pitneybowes.com");
            UserAgent = "Pitney Bowes CSharp SDK 1.0";
            ThrowExceptions = false;
            GetConfigItem = (s) => { return _configs[s]; };
            AddConfigItem  = (k, v) => { _configs.Add(k, v); };
            LogWarning = (s) => { };
            LogError = (s) => { };
            LogConfigError = (s) => { };
            LogDebug = (s) => { };
            GetAPISecret = () => { return new StringBuilder(); };
            SerializationRegistry = new SerializationRegistry();
            Counters = new Dictionary<string, Counters>();

        }
        public SerializationRegistry SerializationRegistry { get; }
        public IHttpRequest Requester { get; set; } // to allow mocking
        public Token AuthToken { get; set; }
        public string UserAgent { get; set; }
        public int TimeOutMilliseconds { get; set; }
        public int Retries { get; set; }
        public bool ThrowExceptions { get; set; }
        public Func<string, string> GetConfigItem { get; set; }
        public Action<string, string> AddConfigItem { get; set; }
        public Action<string> LogWarning { get; set; }
        public Action<string> LogError { get; set; }
        public Action<string> LogConfigError { get; set; }
        public Action<string> LogDebug { get; set; }
        public Func<StringBuilder> GetAPISecret { get; set; }
        public string EndPoint { get; set; }
        public bool Record { get; set; }
        public string RecordPath { get; set; }
        public bool RecordOverwrite { get; set; }

        public Dictionary<string, Counters> Counters { get; set; }

        public void UpdateCounters(string uri, bool success, TimeSpan time)
        {
            if (!Counters.ContainsKey(uri)) Counters.Add(uri, new Counters());
            if (!success) 
            {
                Counters[uri].ErrorCount++;
            }
            else
            {
                Counters[uri].AddCall(time);
            }
        }
    }
}