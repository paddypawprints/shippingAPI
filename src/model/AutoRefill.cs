using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class AutoRefill : IAutoRefill
    {

        public string MerchantID{get; set;}
        public decimal Threshold{get; set;}
        public decimal AddAmount{get; set;}
        public Boolean Enabled{get; set;}
    }
}
