using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi
{
    public interface IShippingApiRequest
    {
        string ContentType {get;}
        StringBuilder Authorization {get;set;}
        string GetUri(string baseUrl);
        IEnumerable<Tuple<ShippingApiHeaderAttribute, string, string>> GetHeaders();
        void SerializeBody(StreamWriter writer, Session session);
        string RecordingSuffix { get; }
        string RecordingFullPath(string resource, Session session);
    }
}