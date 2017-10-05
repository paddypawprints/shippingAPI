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