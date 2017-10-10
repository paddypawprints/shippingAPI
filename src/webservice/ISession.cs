/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

using System;
using System.Text;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface ISession
    {
        Action<string, string> AddConfigItem { get; set; }
        IToken AuthToken { get; set; }
        string EndPoint { get; set; }
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
    }
}