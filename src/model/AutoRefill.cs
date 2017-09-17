using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class AutoRefill : IAutoRefill
    {
        virtual public string MerchantID{get; set;}
        virtual public decimal Threshold{get; set;}
        virtual public decimal AddAmount{get; set;}
        virtual public Boolean Enabled{get; set;}
    }
}
