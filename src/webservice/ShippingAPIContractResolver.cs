using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PitneyBowes.Developer.ShippingApi
{
    internal class ShippingApiContractResolver : DefaultContractResolver
    {

        public static readonly ShippingApiContractResolver Instance = new ShippingApiContractResolver();
        public Session Session { get; set; }

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