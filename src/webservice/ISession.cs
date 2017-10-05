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