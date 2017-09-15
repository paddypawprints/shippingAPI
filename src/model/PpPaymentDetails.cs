using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class PpPaymentDetails : IPpPaymentDetails
    {

        public string EncryptedTIN{get; set;}
        public string EncryptedBPN{get; set;}
    }
}
