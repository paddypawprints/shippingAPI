using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PitneyBowes.Developer.ShippingApi.Json;

namespace PitneyBowes.Developer.ShippingApi
{
    public class SerializationRegistry
    {
        private Dictionary<Type, JsonConverter> _serializationRegistry = new Dictionary<Type, JsonConverter>();
        private Dictionary<Type, Type> _wrapperRegistry = new Dictionary<Type, Type>();

        public SerializationRegistry()
        {
            _serializationRegistry.Add(typeof(SpecialServiceCodes), new SpecialServiceCodesConverter());
            _serializationRegistry.Add(typeof(PackageLocation), new PackageLocationConverter());
            _serializationRegistry.Add(typeof(TrackingStatusCode), new TrackingStatusConverter());
            _serializationRegistry.Add(typeof(TransactionType), new TransactionTypeConverter());

            RegisterSerializationWrappers();
        }

        public JsonConverter GetConverter(Type type)
        {
            if (_serializationRegistry.ContainsKey(type))
            {
                return _serializationRegistry[type];
            }
            return null;
        }

        public void RegisterSerializationTypes<I, T>() where T : I
        {
            Type interfaceType = typeof(I);
            Type objectType = typeof(T);
            Type wrapperType = _wrapperRegistry[interfaceType];
            JsonConverter c = new ShippingApiConverter(objectType, wrapperType.MakeGenericType(new Type[] { objectType }));

            _serializationRegistry[interfaceType] = c;
            _serializationRegistry[objectType] = c;
        }

        private void RegisterSerializationWrappers()
        {
            _wrapperRegistry.Add(typeof(IAddress), typeof(JsonAddress<>));
            _wrapperRegistry.Add(typeof(IAutoRefill), typeof(JsonAutoRefill<>));
            _wrapperRegistry.Add(typeof(ICarrierPickup), typeof(JsonCarrierPickup<>));
            _wrapperRegistry.Add(typeof(ICarrierRule), typeof(JsonCarrierRule<>));
            _wrapperRegistry.Add(typeof(ICcPaymentDetails), typeof(JsonCcPaymentDetails<>));
            _wrapperRegistry.Add(typeof(ICountry), typeof(JsonCountry<>));
            _wrapperRegistry.Add(typeof(ICustoms), typeof(JsonCustoms<>));
            _wrapperRegistry.Add(typeof(ICustomsInfo), typeof(JsonCustomsInfo<>));
            _wrapperRegistry.Add(typeof(ICustomsItems), typeof(JsonCustomsItems<>));
            _wrapperRegistry.Add(typeof(IDeliveryCommitment), typeof(JsonDeliveryCommitment<>));
            _wrapperRegistry.Add(typeof(IDimensionRule), typeof(JsonDimensionRule<>));
            _wrapperRegistry.Add(typeof(IDocument), typeof(JsonDocument<>));
            _wrapperRegistry.Add(typeof(IManifest), typeof(JsonManifest<>));
            _wrapperRegistry.Add(typeof(IMerchant), typeof(JsonMerchant<>));
            _wrapperRegistry.Add(typeof(IParameter), typeof(JsonParameter<>));
            _wrapperRegistry.Add(typeof(IParcel), typeof(JsonParcel<>));
            _wrapperRegistry.Add(typeof(IParcelDimension), typeof(JsonParcelDimension<>));
            _wrapperRegistry.Add(typeof(IParcelTypeRule), typeof(JsonParcelTypeRule<>));
            _wrapperRegistry.Add(typeof(IParcelWeight), typeof(JsonParcelWeight<>));
            _wrapperRegistry.Add(typeof(IPaymentInfo), typeof(JsonPaymentInfo<>));
            _wrapperRegistry.Add(typeof(IPickup), typeof(JsonPickup<>));
            _wrapperRegistry.Add(typeof(IPickupCount), typeof(JsonPickupCount<>));
            _wrapperRegistry.Add(typeof(IPpPaymentDetails), typeof(JsonPpPaymentDetails<>));
            _wrapperRegistry.Add(typeof(IRates), typeof(JsonRates<>));
            _wrapperRegistry.Add(typeof(IServicesParameterRule), typeof(JsonServicesParameterRule<>));
            _wrapperRegistry.Add(typeof(IServicesPrerequisiteRule), typeof(JsonServicesPrerequisiteRule<>));
            _wrapperRegistry.Add(typeof(ISpecialServices), typeof(JsonSpecialServices<>));
            _wrapperRegistry.Add(typeof(ISpecialServicesRule), typeof(JsonSpecialServicesRule<>));
            _wrapperRegistry.Add(typeof(IShipmentOptions), typeof(JsonShipmentOptions<>));
            _wrapperRegistry.Add(typeof(IShipment), typeof(JsonShipment<>));
            _wrapperRegistry.Add(typeof(IToken), typeof(JsonToken<>));
            _wrapperRegistry.Add(typeof(ITransaction), typeof(JsonTransaction<>));
            _wrapperRegistry.Add(typeof(ITrackingEvent), typeof(JsonTrackingEvent<>));
            _wrapperRegistry.Add(typeof(ITrackingStatus), typeof(JsonTrackingStatus<>));
            _wrapperRegistry.Add(typeof(ITransactionSort), typeof(JsonTransactionSort<>));
            _wrapperRegistry.Add(typeof(IUserInfo), typeof(JsonUserInfo<>));
            _wrapperRegistry.Add(typeof(IWeightRule), typeof(JsonWeightRule<>));
        }
    }
}
