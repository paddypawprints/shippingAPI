/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

using System.Net.Http;
using System;
using System.Collections.Generic;


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

    }
}