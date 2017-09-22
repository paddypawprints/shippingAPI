using System.Net.Http;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Serialization;


namespace PitneyBowes.Developer.ShippingApi
{
#if NET_45
    // Differences in net_45 aqnd net_core Newtonsoft
#else
    public class DebugTraceWriter : ITraceWriter
    {
        private Action<string> _writer;

        public DebugTraceWriter(Action<string> writer)
        {
            _writer = writer;
        }

        public TraceLevel LevelFilter => throw new NotImplementedException("JsonConvert TraceLevel");
        TraceLevel ITraceWriter.LevelFilter => throw new NotImplementedException("JsonConvert TraceLevelFilter");
        public void Trace(TraceLevel level, string message, Exception ex)
        {
            _writer("== " + level.ToString() + " " + message + " " + ex.Message);
        }
    }
#endif
    /// <summary>
    /// Various environmental items that get injected into most methods. They can be overridden to configure the system.
    /// </summary>
    public class Session
    {
        public IHttpRequest Requestor { get; set; } // to allow mocking

        public static implicit operator Session(string name)
        {
            return SessionDefaults._defaultSessions[name];
        }
        public string Name { get; set; }
        public IToken AuthToken { get; set; }

        public Session()
        {
            Record = false;
            RecordPath = string.Format("{0}{1}recordings{1}shippingApi", Path.GetTempPath(), Path.DirectorySeparatorChar);
            RecordOverwrite = false;
        }

#if NET_45
#else
    public DebugTraceWriter NewtonSoftTrace { get; set; }
        public bool TraceSerialization
        {
            get { return NewtonSoftTrace == null; }
            set
            {
                if (value && (NewtonSoftTrace == null)) NewtonSoftTrace = new DebugTraceWriter(LogDebug);
                if (!value) NewtonSoftTrace = null;
            }
        }
#endif

        internal Dictionary<Type, JsonConverter> SerializationRegistry = new Dictionary<Type, JsonConverter>();
        internal Dictionary<Type, Type> WrapperRegistry = new Dictionary<Type, Type>();

        private static object _clientLock = new object();
        private static Dictionary<string, HttpClient> _clientLookup = new Dictionary<string, HttpClient>();
        public HttpClient Client(string baseUrl)
        {
            lock (_clientLock)
            {
                if (!_clientLookup.TryGetValue(baseUrl, out HttpClient client))
                {
                    client = new HttpClient() { BaseAddress = new Uri(baseUrl) };
                    _clientLookup.Add(baseUrl, client);
                }
                return client;
            }
        }
        public Func<string, string> GetConfigItem = SessionDefaults.GetConfigItem;
        public Action<string, string> AddConfigItem = SessionDefaults.AddConfigItem;
        public Action<string> LogWarning = SessionDefaults.LogWarning;
        public Action<string> LogError = SessionDefaults.LogError;
        public Action<string> LogConfigError = SessionDefaults.LogConfigError;
        public Action<string> LogDebug = SessionDefaults.LogDebug;
        public Func<char[]> GetAPISecret = SessionDefaults.GetAPISecret;
        public string EndPoint { get; set; }
        public void RegisterSerializationTypes<I, T>() where T : I
        {
            Type interfaceType = typeof(I);
            Type objectType = typeof(T);
            Type wrapperType = SessionDefaults.WrapperRegistry[interfaceType];
            JsonConverter c = new ShippingApiConverter(objectType, wrapperType.MakeGenericType(new Type[] { objectType }), this);

            SerializationRegistry[interfaceType] = c;
            SerializationRegistry[objectType] = c;
        }
        public bool Record { get; set; }
        public string RecordPath { get; set; }
        public bool RecordOverwrite { get; set; }
    }
}