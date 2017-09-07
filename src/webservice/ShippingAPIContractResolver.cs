using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PitneyBowes.Developer.ShippingApi
{
    internal class ShippingAPIContractResolver : DefaultContractResolver
    {

        public static readonly ShippingAPIContractResolver Instance = new ShippingAPIContractResolver();
        public ShippingApi.Session Session { get; set; }

        protected override JsonContract CreateContract(Type objectType )
        {
            JsonContract contract = base.CreateContract(objectType);
    
            // this will only be called once and then cached
            if (Session.SerializationRegistry.ContainsKey(objectType))
            {
                contract.Converter = Session.SerializationRegistry[objectType];
            }
            return contract;
        }

    }

}