using System;
using System.Collections.Generic;
using PitneyBowes.Developer.ShippingApi.Json;
using Newtonsoft.Json;


namespace PitneyBowes.Developer.ShippingApi
{

    public static class SessionDefaults
    {
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
        internal static Dictionary<Type, Type> WrapperRegistry = new Dictionary<Type, Type>();

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
                WrapperRegistry.Add(typeof(IAutoRefill), typeof(JsonAutoRefill<>));
                WrapperRegistry.Add(typeof(ICarrierPickup), typeof(JsonCarrierPickup<>));
                WrapperRegistry.Add(typeof(ICarrierRule), typeof(JsonCarrierRule<>));
                WrapperRegistry.Add(typeof(ICcPaymentDetails), typeof(JsonCcPaymentDetails<>));
                WrapperRegistry.Add(typeof(ICountry), typeof(JsonCountry<>));
                WrapperRegistry.Add(typeof(ICustoms), typeof(JsonCustoms<>));
                WrapperRegistry.Add(typeof(ICustomsInfo), typeof(JsonCustomsInfo<>));
                WrapperRegistry.Add(typeof(ICustomsItems), typeof(JsonCustomsItems<>));
                WrapperRegistry.Add(typeof(IDeliveryCommitment), typeof(JsonDeliveryCommitment<>));
                WrapperRegistry.Add(typeof(IDimensionRule), typeof(JsonDimensionRule<>));
                WrapperRegistry.Add(typeof(IDocument), typeof(JsonDocument<>));
                WrapperRegistry.Add(typeof(IManifest), typeof(JsonManifest<>));
                WrapperRegistry.Add(typeof(IMerchant), typeof(JsonMerchant<>));
                WrapperRegistry.Add(typeof(IParameter), typeof(JsonParameter<>));
                WrapperRegistry.Add(typeof(IParcel), typeof(JsonParcel<>));
                WrapperRegistry.Add(typeof(IParcelDimension), typeof(JsonParcelDimension<>));
                WrapperRegistry.Add(typeof(IParcelTypeRule), typeof(JsonParcelTypeRule<>));
                WrapperRegistry.Add(typeof(IParcelWeight), typeof(JsonParcelWeight<>));
                WrapperRegistry.Add(typeof(IPaymentInfo), typeof(JsonPaymentInfo<>));
                WrapperRegistry.Add(typeof(IPickup), typeof(JsonPickup<>));
                WrapperRegistry.Add(typeof(IPickupCount), typeof(JsonPickupCount<>));
                WrapperRegistry.Add(typeof(IPpPaymentDetails), typeof(JsonPpPaymentDetails<>));
                WrapperRegistry.Add(typeof(IRates), typeof(JsonRates<>));
                WrapperRegistry.Add(typeof(IServicesParameterRule), typeof(JsonServicesParameterRule<>));
                WrapperRegistry.Add(typeof(IServicesPrerequisiteRule), typeof(JsonServicesPrerequisiteRule<>));
                WrapperRegistry.Add(typeof(ISpecialServices), typeof(JsonSpecialServices<>));
                WrapperRegistry.Add(typeof(ISpecialServicesRule), typeof(JsonSpecialServicesRule<>));
                WrapperRegistry.Add(typeof(IShipmentOptions), typeof(JsonShipmentOptions<>));
                WrapperRegistry.Add(typeof(IShipment), typeof(JsonShipment<>));
                WrapperRegistry.Add(typeof(IToken), typeof(JsonToken<>));
                WrapperRegistry.Add(typeof(ITransaction), typeof(JsonTransaction<>));
                WrapperRegistry.Add(typeof(ITrackingEvent), typeof(JsonTrackingEvent<>));
                WrapperRegistry.Add(typeof(ITrackingStatus), typeof(JsonTrackingStatus<>));
                WrapperRegistry.Add(typeof(ITransactionSort), typeof(JsonTransactionSort<>));
                WrapperRegistry.Add(typeof(IUserInfo), typeof(JsonUserInfo<>));
                WrapperRegistry.Add(typeof(IWeightRule), typeof(JsonWeightRule<>));
            }
        }

        internal static Dictionary<string, Session >  _defaultSessions = new Dictionary<string, Session>();
    }
}