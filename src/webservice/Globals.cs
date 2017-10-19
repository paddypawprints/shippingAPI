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

using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;


namespace PitneyBowes.Developer.ShippingApi
{
    public static class Globals
    {
        private static object _clientLock = new object();
        public static ISession DefaultSession { get; set; } 

        private static Dictionary<string, HttpClient> _clientLookup = new Dictionary<string, HttpClient>();
        public static HttpClient Client(string baseUrl)
        {
            if (!_clientLookup.TryGetValue(baseUrl, out HttpClient client))
            {
                lock (_clientLock)
                {
                    if (!_clientLookup.TryGetValue(baseUrl, out client))
                    {
                        client = new HttpClient() { BaseAddress = new Uri(baseUrl) };
                        _clientLookup.Add(baseUrl, client);
                    }
                    return client;
                }
            }
            else
            {
                return client;
            }
        }
        // there is undoutedly a better place to put this, just cant think of it
        public static string GetPath(params string[] folders)
        {
            var sb = new StringBuilder();
            foreach (var s in folders)
            {
                sb.Append(s);
                sb.Append(Path.DirectorySeparatorChar);
            }
            return sb.ToString();
        }
        public static string GetConfigPath()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Environment.GetEnvironmentVariable("APPDATA");
            }
            else
            {
                return Environment.GetEnvironmentVariable("HOME");
            }
        }
        public static string GetConfigFilePrefix()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return "";
            }
            else
            {
                return ".";
            }            
        }
    }
}