using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PitneyBowes.Developer.ShippingApi
{
    internal class ShippingApiContractResolver : DefaultContractResolver
    {

        public static readonly ShippingApiContractResolver Instance = new ShippingApiContractResolver();
        public SerializationRegistry Registry { get; set; }

        protected override JsonContract CreateContract(Type objectType )
        {
            // this will only be called once and then cached 
            JsonContract contract = base.CreateContract(objectType);
            var c = Registry.GetConverter(objectType);
            if (c != null)
                contract.Converter = c;
            return contract;
        }

    }

}