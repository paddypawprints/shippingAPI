using System.Net.Http;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using PitneyBowes.Developer.ShippingApi.Json;
using Newtonsoft.Json;


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
            _writer( "== " + level.ToString() + " " + message + " " + ex.Message);
        }
    }
#endif
    public static class ShippingApi
    {
        public class Session
        {
            public IHttpRequest Requestor { get; set; } // to allow mocking

            public static implicit operator Session(string name)
            {
                return ShippingApi._defaultSessions[name];
            }
            public string Name { get; set; }
            public IToken AuthToken { get; set; }

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
            public Func<string, string> GetConfigItem = ShippingApi.GetConfigItem;
            public Action<string, string> AddConfigItem = ShippingApi.AddConfigItem;
            public Action<string> LogWarning = ShippingApi.LogWarning;
            public Action<string> LogError = ShippingApi.LogError;
            public Action<string> LogConfigError = ShippingApi.LogConfigError;
            public Action<string> LogDebug = ShippingApi.LogDebug;
            public Func<char[]> GetAPISecret = ShippingApi.GetAPISecret;
            public string EndPoint { get; set; }
            public void RegisterSerializationTypes<I, T>() where T : I
            {
                Type interfaceType = typeof(I);
                Type objectType = typeof(T);
                Type wrapperType = ShippingApi.WrapperRegistry[interfaceType];
                JsonConverter c = new ShippingApiConverter(objectType, wrapperType.MakeGenericType(new Type[] { objectType }), this);

                SerializationRegistry[interfaceType] = c;
                SerializationRegistry[objectType] = c;
                //WrapperRegistry[objectType] = wrapperType;
            }
        }
        public static Func<string, string> GetConfigItem = (s) => { return _defaultConfigs[s]; };
        public static Action<string, string> AddConfigItem = (k, v) => { _defaultConfigs.Add(k, v); };
        public static Action<string> LogWarning = (s) => { };
        public static Action<string> LogError = (s) => { };
        public static Action<string> LogConfigError = (s) => { };
        public static Action<string> LogDebug = (s) => { };
        public static Func<char[]> GetAPISecret = () => { return new char[1] { '0' }; };

        private static Dictionary<string, string> _defaultConfigs = new Dictionary<string, string>();

        public static Session DefaultSession;

        private static object _initLock = new object();
        private static Dictionary<Type, Type> WrapperRegistry = new Dictionary<Type, Type>();

        public static void Init()
        { 
            lock(_initLock)
            {
                _defaultConfigs.Add("SANDBOX_ENDPOINT", "https://api-sandbox.pitneybowes.com");
                _defaultConfigs.Add("PRODUCTION_ENDPOINT", "https://api-sandbox.pitneybowes.com");

                var sandbox = new Session() { Name = "sandbox", EndPoint = GetConfigItem("SANDBOX_ENDPOINT"), Requestor = new ShippingApiHttpRequest()};
                var production = new Session() { Name= "production", EndPoint = GetConfigItem("PRODUCTION_ENDPOINT"), Requestor = new ShippingApiHttpRequest() };
                _defaultSessions.Add("sandbox", sandbox );
                _defaultSessions.Add("production", production );
                DefaultSession = sandbox;
                WrapperRegistry.Add(typeof(IAddress), typeof(JsonAddress<>));
                WrapperRegistry.Add(typeof(IRates), typeof(JsonRates<>));
                WrapperRegistry.Add(typeof(IParcel), typeof(JsonParcel<>));
                WrapperRegistry.Add(typeof(ICustoms), typeof(JsonCustoms<>));
                WrapperRegistry.Add(typeof(ICustomsInfo), typeof(JsonCustomsInfo<>));
                WrapperRegistry.Add(typeof(ICustomsItems), typeof(JsonCustomsItems<>));
                WrapperRegistry.Add(typeof(IParcelDimension), typeof(JsonParcelDimension<>));
                WrapperRegistry.Add(typeof(IParcelWeight), typeof(JsonParcelWeight<>));
                WrapperRegistry.Add(typeof(IDocument), typeof(JsonDocument<>));
                WrapperRegistry.Add(typeof(IShipmentOptions), typeof(JsonShipmentOptions<>));
                WrapperRegistry.Add(typeof(ISpecialServices), typeof(JsonSpecialServices<>));
                WrapperRegistry.Add(typeof(IToken), typeof(JsonToken<>));
                WrapperRegistry.Add(typeof(ICountry), typeof(JsonCountry<>));
                WrapperRegistry.Add(typeof(IDeliveryCommitment), typeof(JsonDeliveryCommitment<>));
                WrapperRegistry.Add(typeof(IManifest), typeof(JsonManifest<>));
                WrapperRegistry.Add(typeof(IMerchant), typeof(JsonMerchant<>));
                WrapperRegistry.Add(typeof(IParameter), typeof(JsonParameter<>));
                WrapperRegistry.Add(typeof(IShipment), typeof(JsonShipment<>));
                WrapperRegistry.Add(typeof(ITransaction), typeof(JsonTransaction<>));
                WrapperRegistry.Add(typeof(ITransactionSort), typeof(JsonTransactionSort<>));
                WrapperRegistry.Add(typeof(IPickup), typeof(JsonPickup<>));
                WrapperRegistry.Add(typeof(IPickupCount), typeof(JsonPickupCount<>));
                WrapperRegistry.Add(typeof(IUserInfo), typeof(JsonUserInfo<>));
                WrapperRegistry.Add(typeof(IPaymentInfo), typeof(JsonPaymentInfo<>));
                WrapperRegistry.Add(typeof(IPpPaymentDetails), typeof(JsonPpPaymentDetails<>));
                WrapperRegistry.Add(typeof(ICcPaymentDetails), typeof(JsonCcPaymentDetails<>));
                WrapperRegistry.Add(typeof(IAutoRefill), typeof(JsonAutoRefill<>));
            }
        }

        internal static Dictionary<string, Session >  _defaultSessions = new Dictionary<string, Session>();
    }
}