﻿/*
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
using System.Text;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface ISession
    {
        Action<string, string> AddConfigItem { get; set; }
        Token AuthToken { get; set; }
        string EndPoint { get; set; }
        string UserAgent { get; set; }
        bool ThrowExceptions { get; set; }
        Func<StringBuilder> GetAPISecret { get; set; }
        Func<string, string> GetConfigItem { get; set; }
        Action<string> LogConfigError { get; set; }
        Action<string> LogDebug { get; set; }
        Action<string> LogError { get; set; }
        Action<string> LogWarning { get; set; }
        bool Record { get; set; }
        bool RecordOverwrite { get; set; }
        string RecordPath { get; set; }
        IHttpRequest Requester { get; set; }
        int Retries { get; set; }
        SerializationRegistry SerializationRegistry { get; }
        Dictionary<string, Counters> Counters { get; set; }
        void UpdateCounters(string uri, bool success, TimeSpan time);
    }
}