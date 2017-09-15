using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class CcPaymentDetails : ICcPaymentDetails
    {

        public CreditCardType CcType{get; set;}
        public string CcTokenNumber{get; set;}
        public string CcExpirationDate{get; set;}
        public string CccvvNumber{get; set;}
        public IAddress CcAddress{get; set;}
    }
}
