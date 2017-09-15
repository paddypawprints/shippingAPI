using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi.Model
{
    public class PaymentInfo : IPaymentInfo
    {

        public PaymentType PaymentType{get; set;}
        public PaymentMethod PaymentMethod{get; set;}
        public IPpPaymentDetails PpPaymentDetails{get; set;}
        public ICcPaymentDetails CcPaymentDetails{get; set;}
    }
}
